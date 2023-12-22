using finance.DLL.Models;
using System.ComponentModel.DataAnnotations;

namespace finance.MVC.Models
{
    public class HistoryViews
    {
        public int HistoryId { get; set; }

        public TransactionTypeViews TransactionType { get; set; }

        public decimal LeftBalance { get; set; }
        public string Price { get; set; }
        public string WalletFrom { get; set; }
        public string Category { get; set; }
        public string WalletTo { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
