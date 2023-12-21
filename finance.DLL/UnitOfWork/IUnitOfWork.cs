using System;
using finance.DLL.Models;
using finance.DLL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace finance.DLL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<int, Wallet> WalletsRepository { get; }
        IRepository<int, Transaction> TransactionsRepository { get; }
        IRepository<int, History> HistoryRepository { get; }
        IRepository<int, FinancialCategory> FinancialCategoryRepository { get; }
        IRepository<int, TransactionType> TransactionTypeRepository { get; }

        event EventHandler<PropertyValues> CantSaveChanges;

        void Save();

    }
}
