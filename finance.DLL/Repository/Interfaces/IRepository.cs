using finance.DLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace finance.DLL.Repository.Interfaces
{
    public interface IRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        IEnumerable<TEntity> GetAll();
        TEntity? Get(TKey id);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, Boolean>> predicate);
        void Create(TEntity item);
        void Update(TEntity item);
        void UpdateRange(List<TEntity> items);
        void Delete(TKey id);
    }
}
