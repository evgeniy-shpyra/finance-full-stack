using finance.DLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finance.DLL.Repository
{
    public class TransactionTypeRepository : BaseRepository<int, TransactionType>
    {
        public TransactionTypeRepository(FinanceContext context) : base(context) { }

        public override IEnumerable<TransactionType> GetAll()
        {
            return db.Set<TransactionType>().Include(gs => gs.Transactions).AsNoTracking();
        }
    }
}
