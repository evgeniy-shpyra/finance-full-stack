using finance.DLL.Models;
using finance.DLL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace finance.DLL.Repository
{
    public class TransactionsRepository : BaseRepository<int, Transaction>
    {
        public TransactionsRepository(FinanceContext context) : base(context) { }

        public override IEnumerable<Transaction> GetAll()
        {
            return db.Set<Transaction>()
                .Include(gs => gs.TransactionType)
                .Include(gs => gs.FinancialCategory)
                .Include(gs => gs.SendingWallet)
                .Include(gs => gs.ReceivingWallet)
                .AsNoTracking();
        }
    }
}
