using finance.DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finance.BLL.ModelsDTO
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        public WalletDTO SendingWallet { get; set; }
        public WalletDTO ReceivingWallet { get; set; }

        public FinancialCategoryDTO FinancialCategory { get; set; } 
        public TransactionTypeDTO TransactionType { get; set; }

    }
}
