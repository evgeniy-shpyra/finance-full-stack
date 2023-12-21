using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finance.BLL.ModelsDTO
{
    public class HistoryDTO
    {
        public int Id { get; set; }

        public decimal LeftBalance { get; set; }
        public string Price { get; set; }
        public string WalletFrom { get; set; }
        public string WalletTo { get; set; }
        public string Category { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
