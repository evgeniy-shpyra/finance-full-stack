using finance.DLL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace finance.DLL.Models
{
    public class Wallet : BaseEntity<int>
    {
        public string Name { get; set; } = null!;

        public decimal Balance { get; set; }

        public ICollection<Transaction> SendTransactions { get; set; }
        public ICollection<Transaction> ReceivedTransactions { get; set; }
    }
}
