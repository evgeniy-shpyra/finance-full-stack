using finance.DLL.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace finance.DLL.Models
{
    public class History : BaseEntity<int>
    {
        public decimal LeftBalance { get; set; }
        public decimal Price { get; set; }
        public string? CategoryName { get; set; } = null;
        public string? WalletFromName { get; set; } = null;
        public string? WalletToName { get; set; }

        public int TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
