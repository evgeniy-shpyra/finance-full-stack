using finance.DLL.Entities;
using System.Collections.Generic;

namespace finance.DLL.Models
{
    public class FinancialCategory : BaseEntity<int>
    {
        public string Name { get; set; } = null!;

        public ICollection<Transaction> Transactions { get; set; }
    }
}
