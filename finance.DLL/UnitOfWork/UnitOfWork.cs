using finance.DLL.Models;
using finance.DLL.Repository;
using finance.DLL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace finance.DLL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public event EventHandler<PropertyValues> CantSaveChanges = null!;

        private readonly FinanceContext context;
        private IRepository<int, Wallet> wallets;
        private IRepository<int, Transaction> transactions;
        private IRepository<int, History> history;
        private IRepository<int, FinancialCategory> categories;
        private IRepository<int, TransactionType> types;


        public IRepository<int, Wallet> WalletsRepository
        {
            get
            {
                if (wallets == null)
                {
                    wallets = new WalletsRepository(context);
                }

                return wallets;
            }
        }
        public IRepository<int, Transaction> TransactionsRepository
        {
            get
            {
                if (transactions == null)
                {
                    transactions = new TransactionsRepository(context);
                }

                return transactions;
            }
        }
        public IRepository<int, History> HistoryRepository
        {
            get
            {
                if (history == null)
                {
                    history = new Repository.HistoryRepository(context);
                }

                return history;
            }
        }
        public IRepository<int, FinancialCategory> FinancialCategoryRepository
        {
            get
            {
                if (categories == null)
                {
                    categories = new FinancialCategoryRepository(context);
                }
                return categories;
            }
        }
        public IRepository<int, TransactionType> TransactionTypeRepository
        {
            get
            {
                if (types == null)
                {
                    types = new TransactionTypeRepository(context);
                }

                return types;
            }
        }

        public UnitOfWork(FinanceContext context)
        {
            this.context = context;
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    if (entry.Entity is Transaction ||
                        entry.Entity is FinancialCategory ||
                        entry.Entity is TransactionType ||
                        entry.Entity is Wallet ||
                        entry.Entity is History)
                    {
                        var proposedValues = entry.CurrentValues;
                        CantSaveChanges?.Invoke(this, proposedValues);
                    }
                    else
                    {
                        throw new NotSupportedException(
                            "Don't know how to handle concurrency conflicts for "
                            + entry.Metadata.Name);
                    }
                }
            }
            finally
            {
                context.ChangeTracker?.Clear();
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
