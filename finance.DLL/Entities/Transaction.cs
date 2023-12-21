using finance.DLL.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace finance.DLL.Models
{
    public class Transaction : BaseEntity<int>
    {
 
        public decimal Price { get; set; }

        public int? SendingWalletId { get; set; }
        public Wallet? SendingWallet { get; set; }

        public int? ReceivingWalletId { get; set; }
        public Wallet? ReceivingWallet { get; set; }

        public int FinancialCategoryId { get; set; }

        public FinancialCategory FinancialCategory { get; set; }
        public int TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; }
       // public int HistoryRecordId { get; set; }
       // public History HistoryRecord { get; set; }
    }
}
