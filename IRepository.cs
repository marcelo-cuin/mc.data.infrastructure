using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace mc.data.infrastructure
{
    public interface IRepository<T> where T : IEntity
    {
        Task<List<T>> List();
        Task<List<T>> List(Expression<Func<T, bool>> predicate);
        Task Insert(T entity, bool commit = false);
        Task Delete(T entity, bool commit = false);
        Task Update(T entity, bool commit = false);
        Task<T> Find(Expression<Func<T, bool>> predicate);
        Task<T> Find(params object[] keyValues);
    }
}
