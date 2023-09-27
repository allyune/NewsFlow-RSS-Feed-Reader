using System;
using Microsoft.Extensions.Logging;
using NewsFlow.Data.Infrastructure;
using NewsFlow.Data.Models;

namespace NewsFlow.Data.Repositories.FeedRepository
{
    public class AsyncFeedRepository : AsyncRepository<Feed>, IAsyncFeedRepository
    {
        private readonly RssDbContext _dbContext;
        private readonly ILogger<AsyncFeedRepository> _logger;

        public AsyncFeedRepository(
            RssDbContext dbContext, ILogger<AsyncFeedRepository> logger)
            : base(dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
    }
}

