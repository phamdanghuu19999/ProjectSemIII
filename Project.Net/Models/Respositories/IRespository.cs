using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Net.Models.Respositories
{
    interface IRespository<TEntity> where TEntity:class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(object id);
        IEnumerable<TEntity> GetBy(Func<TEntity, bool> predicate);
        TEntity SingleBy(Func<TEntity, bool> predicate);
        bool Add(TEntity entity);
        bool AddRange(IEnumerable<TEntity> entities);
        bool Edit(TEntity entity);
        bool Remove(object id);
        bool Remove(TEntity entity);
        bool RemoveRange(IEnumerable<TEntity> entities);
    }
}
