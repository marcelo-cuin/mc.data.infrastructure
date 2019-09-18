using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;
using connector.infrastructure.Interfaces;

namespace connector.infrastructure
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        private DbContext _dbContext;

        public Repository(DbContext context)
        {
            _dbContext = context;
        }

        async public virtual Task<List<T>> List()
        {
            return await _dbContext.Set<T>().ToListAsync<T>();
        }

        async public virtual Task<List<T>> List(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>()
                         .Where(predicate)
                         .ToListAsync<T>();
        }

        async public virtual Task<T> Find(Expression<Func<T, bool>> predicate)
        {
            T result = null;

            var lista = await this.List(predicate);

            if (lista != null && lista.Count > 0)
            {
                result = lista[0];
            }

            return result;
        }

        async public virtual Task<T> Find(params object[] keyValues)
        {
            return await _dbContext.Set<T>().FindAsync(keyValues);
        }

        async public virtual Task Insert(T entity, bool commit = false)
        {
            _dbContext.Set<T>().Add(entity);
            if (commit)
            {
                await _dbContext.SaveChangesAsync();
            }
        }

        async public virtual Task Update(T entity, bool commit = false)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            if (commit)
            {
                await _dbContext.SaveChangesAsync();
            }
        }

        async public virtual Task Delete(T entity, bool commit = false)
        {
            _dbContext.Set<T>().Remove(entity);
            if (commit)
            {
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
