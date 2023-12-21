using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finance.BLL.ModelsDTO
{
    public class CreateTransactionDTO
    {
        public decimal Price { get; set; }

        public int? SendingWalletId { get; set; } = null;
        public int? ReceivingWalletId { get; set; } = null;

        public int? FinancialCategoryId{ get; set; } = null;
    }
}
