using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ABCBrasil.Hackthon.Api.Domain.Interfaces.Repository
{
    public interface IRepositoryBase<T> where T : class
    {
        public Task<T> AddAsync(T entity);

        public Task<T> GetByIdAsync(string id);

        public Task<IEnumerable<T>> GetAllAsync();

        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        public Task UpdateAsync(T entity);

        public Task DeleteAsync(T entity);
    }
}