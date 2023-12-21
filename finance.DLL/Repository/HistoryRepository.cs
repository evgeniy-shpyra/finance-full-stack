using finance.DLL.Models;
using finance.DLL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace finance.DLL.Repository
{
    public class HistoryRepository : BaseRepository<int, History>
    {
        public HistoryRepository(FinanceContext context) : base(context) { }

        public override IEnumerable<History> GetAll()
        {
            /*
            return db.Set<History>()
                .Include(gs => gs.Transaction)
                    .ThenInclude(t => t.SendingWallet)
                .Include(gs => gs.Transaction)
                    .ThenInclude(t => t.ReceivingWallet)
                .AsNoTracking();
            */

            return db.Set<History>()
                .Include(gs => gs.TransactionType)
                 .AsNoTracking();
        }

    }

}
