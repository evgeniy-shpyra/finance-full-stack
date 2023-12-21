using finance.DLL.Models;
using finance.DLL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace finance.DLL.Repository
{
    public class FinancialCategoryRepository : BaseRepository<int, FinancialCategory>
    {
        public FinancialCategoryRepository(FinanceContext context) : base(context) { }

        public override IEnumerable<FinancialCategory> GetAll()
        {
            return db.Set<FinancialCategory>().Include(gs => gs.Transactions).AsNoTracking();
        }
    }
}
