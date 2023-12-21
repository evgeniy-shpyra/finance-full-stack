using finance.DLL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace finance.DLL.Models
{
    public class TransactionType : BaseEntity<int>
    {
        public string Name { get; set; } = null!;

        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<History> Historys { get; set; }
    }
}