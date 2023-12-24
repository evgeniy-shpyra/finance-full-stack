using finance.DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finance.WebAPI.DTO
{
    public class TransactionViewsDTO
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        public WalletViewsDTO SendingWallet { get; set; }
        public WalletViewsDTO ReceivingWallet { get; set; }

        public FinancialCategoryViewsDTO FinancialCategory { get; set; } 
        public TransactionTypeViewsDTO TransactionType { get; set; }
    }
}
