using AutoMapper;
using finance.BLL.ModelsDTO;
using finance.BLL.Services.Interfaces;
using finance.DLL.Models;
using finance.DLL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace finance.bll.Services
{
    public class WalletService : IWalletService
    {
        private List<string> reservedNames = new List<string> { "systemReceivingWallet", "systemSendingWallet" };
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public WalletService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<WalletDTO> GetAll()
        {
           var wallets = unitOfWork.WalletsRepository
                .GetAll()
                .Where(wallet => !reservedNames.Contains(wallet.Name))
                .ToList();

            var walletsDTOs = _mapper.Map<List<Wallet>, List<WalletDTO>>(wallets);
            return walletsDTOs;
        }

       
        public void Add(CreateWalletDTO wallet)
        {
            if (reservedNames.Contains(wallet.Name))
            {
                throw new Exception("You can't create wallet with this name");
            }

            var newWallet = _mapper.Map<CreateWalletDTO, Wallet>(wallet);

            unitOfWork.WalletsRepository.Create(newWallet);
            unitOfWork.Save();
        }

        public void Remove(int id)
        {
            var reservedWallets = unitOfWork.WalletsRepository
              .GetAll()
              .ToList().FindAll(c => reservedNames.Contains(c.Name));

            if (reservedWallets.Any(wallet => wallet.Id == id))
            {
                throw new Exception("You can't delete this wallet");
            }

            var relatedTransactions = unitOfWork.TransactionsRepository.GetAll()
                .Where(t =>  t.ReceivingWalletId == id)
                .ToList();

            foreach (var transaction in relatedTransactions)
            {
                unitOfWork.TransactionsRepository.Delete(transaction.Id);
            }
           
            unitOfWork.WalletsRepository.Delete(id);
            unitOfWork.Save();
        }
      
    }
}
