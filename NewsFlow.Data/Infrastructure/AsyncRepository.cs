using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NewsFlow.Data.Models;
using NewsFlow.Data.NewsFlowDbContext;

namespace NewsFlow.Data.Infrastructure
{
	public abstract class AsyncRepository<T> : IAsyncRepository<T> where T : BaseDbModel
	{
        private readonly RssDbContext _dbContext;
		private readonly DbSet<T> _dbSet;

		public AsyncRepository(RssDbContext dbContext)
		{
            _dbContext = dbContext;
			_dbSet = dbContext.Set<T>();
		}

        Task<T?> IAsyncRepository<T>.GetAsync(
			Expression<Func<T, bool>> expression)
        {
            return _dbSet.FirstOrDefaultAsync(expression);
        }

        async Task<bool> IAsyncRepository<T>.CheckExists(
            Expression<Func<T, bool>> expression)
        {
            var exists = await _dbSet.AnyAsync(expression);
            return exists;
        }

        Task<List<T>> IAsyncRepository<T>.ListAsync(
            Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).ToListAsync();
        }

        async Task IAsyncRepository<T>.AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        Task<bool> IAsyncRepository<T>.DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.FromResult(true);
        }

        async Task<int> IAsyncRepository<T>.SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}

