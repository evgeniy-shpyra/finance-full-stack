using finance.DLL.Models;
using finance.DLL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace finance.DLL.Repository
{
    public class WalletsRepository : BaseRepository<int, Wallet>
    {
        public WalletsRepository(FinanceContext context) : base(context) { }

        public override IEnumerable<Wallet> GetAll()
        {  
            return db.Set<Wallet>()
                .Include(gs => gs.SendTransactions)
                .Include(gs => gs.ReceivedTransactions)
                .AsNoTracking();
        }
    }
}
