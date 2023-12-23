using AutoMapper;
using finance.BLL.ModelsDTO;
using finance.BLL.Services.Interfaces;
using finance.DLL.Models;
using finance.DLL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace finance.bll.Services
{

    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<TransactionDTO> GetAll()
        {
            var transactions = unitOfWork.TransactionsRepository
                .GetAll()
                .ToList();

            var transactionsDTOs = _mapper.Map<List<Transaction>, List<TransactionDTO>>(transactions);

            return transactionsDTOs;
        }

        public List<TransactionTypeDTO> GetAllTypes()
        {
            var types = unitOfWork
                .TransactionTypeRepository
                .GetAll()
                .ToList();

            var typesDTOs = _mapper.Map<List<TransactionType>, List<TransactionTypeDTO>>(types);

            return typesDTOs;
        }

        public void Add(CreateTransactionDTO transaction)
        {

            var newTransaction = _mapper.Map<CreateTransactionDTO, Transaction>(transaction);

            if (transaction.SendingWalletId.HasValue)
            {
                var walletFrom = unitOfWork.WalletsRepository.Get((int)transaction.SendingWalletId);
                if (walletFrom == null)
                {
                    throw new Exception($"Wallet with id {transaction.SendingWalletId} dosn't exist");
                }
                newTransaction.SendingWallet = walletFrom;
            }
            if (transaction.ReceivingWalletId.HasValue)
            {
                var walletTo = unitOfWork.WalletsRepository.Get((int)transaction.ReceivingWalletId);
                if (walletTo == null)
                {
                    throw new Exception($"Wallet with id {transaction.ReceivingWalletId} dosn't exist");
                }
                newTransaction.ReceivingWallet = walletTo;
            }   
            if (transaction.FinancialCategoryId.HasValue)
            {
                var category = unitOfWork.FinancialCategoryRepository.Get((int)transaction.FinancialCategoryId);
                if (category == null)
                {
                    throw new Exception($"Wallet with id {transaction.ReceivingWalletId} dosn't exist");
                }
                newTransaction.FinancialCategory = category;
                newTransaction.FinancialCategoryId = category.Id;
            }

            History history = new History{ Price = newTransaction.Price };

            var transactionTypes = unitOfWork
                .TransactionTypeRepository
                .GetAll()
                .ToList();

            var wallets = unitOfWork.WalletsRepository.GetAll().ToList();
            decimal leftBalance = 0;
            if (newTransaction.SendingWallet != null && newTransaction.ReceivingWallet != null)
            {
                // bettwen wallet
                leftBalance = newTransaction.SendingWallet.Balance - transaction.Price;
                newTransaction.FinancialCategory = unitOfWork.FinancialCategoryRepository.Get(1);
                newTransaction.TransactionTypeId = transactionTypes.FirstOrDefault(t => t.Name == "between wallets").Id;


                // update wallets
                if (newTransaction.SendingWallet.Balance >= newTransaction.Price)
                {
                    newTransaction.SendingWallet.Balance -= newTransaction.Price;
                    newTransaction.ReceivingWallet.Balance += newTransaction.Price;

                    history.WalletFromName = newTransaction.SendingWallet.Name;
                    history.WalletToName = newTransaction.ReceivingWallet.Name;

                    unitOfWork.WalletsRepository.Update(newTransaction.ReceivingWallet);
                    unitOfWork.WalletsRepository.Update(newTransaction.SendingWallet);
                }
                else
                {
                    throw new Exception("sending wallet doesn't have enough money");
                }
            }
            else if (newTransaction.ReceivingWallet != null && newTransaction.SendingWallet == null)
            {
                // get money
                newTransaction.TransactionTypeId = transactionTypes.Find(t => t.Name == "replenishment").Id;
                newTransaction.SendingWalletId = wallets.Find(t => t.Name == "systemSendingWallet").Id;
                leftBalance = newTransaction.ReceivingWallet.Balance + newTransaction.Price;
                history.WalletToName = newTransaction.ReceivingWallet.Name;
                history.WalletFromName = "";
                
                // update wallet 
                newTransaction.ReceivingWallet.Balance += newTransaction.Price;
                unitOfWork.WalletsRepository.Update(newTransaction.ReceivingWallet);

            }
            else if (newTransaction.ReceivingWallet == null && newTransaction.SendingWallet != null)
            {
                // buy something
                newTransaction.TransactionTypeId = transactionTypes.Find(t => t.Name == "withdrawal").Id;
                newTransaction.ReceivingWalletId = wallets.Find(t => t.Name == "systemReceivingWallet").Id;
                leftBalance = newTransaction.SendingWallet.Balance - newTransaction.Price;
                history.WalletToName = "";
                history.WalletFromName = newTransaction.SendingWallet.Name;

                // update wallet 
                newTransaction.SendingWallet.Balance -= transaction.Price;
                unitOfWork.WalletsRepository.Update(newTransaction.SendingWallet);

            }

            history.LeftBalance = leftBalance;
            history.TransactionTypeId = newTransaction.TransactionTypeId;
            history.TransactionType = newTransaction.TransactionType;

            history.CategoryName = newTransaction.FinancialCategory.Name;


            unitOfWork.TransactionsRepository.Create(newTransaction);

            unitOfWork.HistoryRepository.Create(history);

            unitOfWork.Save();

        }

        public TransactionDTO GetById(int id)
        {
            var transaction = unitOfWork.TransactionsRepository.Get(id);

            var transactionDto = _mapper.Map<Transaction, TransactionDTO>(transaction);

            return transactionDto;
        }

    }

}
