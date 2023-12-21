using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finance.BLL.ModelsDTO
{
    public class WalletDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null;
        public decimal Balance { get; set; } = 0;
    }
}
