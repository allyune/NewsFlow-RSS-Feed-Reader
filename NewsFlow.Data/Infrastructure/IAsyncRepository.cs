using System;
using System.Linq.Expressions;
using NewsFlow.Data.Models;

namespace NewsFlow.Data.Infrastructure
{
    public interface IAsyncRepository<T> where T : BaseDbModel
    {
        Task<T?> GetAsync(Expression<Func<T, bool>> expression);

        Task<bool> CheckExists(Expression<Func<T, bool>> expression);

        Task<List<T>> ListAsync(Expression<Func<T, bool>> expression);

        Task AddAsync(T entity);

        Task<bool> DeleteAsync(T entity);

        Task<int> SaveChangesAsync();
    }
}

