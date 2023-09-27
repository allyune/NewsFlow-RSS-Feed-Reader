using System;
using Microsoft.Extensions.Logging;
using NewsFlow.Data.Models;
using NewsFlow.Data.Repositories.FeedRepository;

namespace NewsFlow.Application.UseCases.DeleteFeeds
{
	public class DeleteFeeds : IDeleteFeeds
	{
        private readonly IAsyncFeedRepository _feedRepository;
        private readonly ILogger<DeleteFeeds> _logger;

        public DeleteFeeds(
            IAsyncFeedRepository repository,
            ILogger<DeleteFeeds> logger)
        {
            _feedRepository = repository;
            _logger = logger;
        }

        public async Task DeleteFeed(Guid id)
		{
            Feed? feed = await _feedRepository.GetAsync(f => f.Id == id);
            if (feed is null)
            {
                throw new FeedNotFoundException($"Feed with Id {id} not found");
            }
            await _feedRepository.DeleteAsync(feed);
            await _feedRepository.SaveChangesAsync();
		}
	}
}

