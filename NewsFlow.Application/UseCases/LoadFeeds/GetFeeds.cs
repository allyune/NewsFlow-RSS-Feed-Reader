using System;
using Microsoft.Extensions.Logging;
using NewsFlow.Data.Models;
using NewsFlow.Data.Repositories.FeedRepository;

namespace NewsFlow.Application.UseCases.LoadFeeds
{
	public class GetFeeds : IGetFeeds
    {
		private readonly IAsyncFeedRepository _feedRepository;
		private readonly ILogger<GetFeeds> _logger;

		public GetFeeds(
			IAsyncFeedRepository repository, ILogger<GetFeeds> logger)
		{
			_feedRepository = repository;
			_logger = logger;
		}
        public async Task<List<Feed>> ListFeeds()
		{
			return await _feedRepository.ListAllAsync();
		}
		public async Task<Feed> GetFeed(Guid id)
		{
			Feed? feed = await _feedRepository.GetAsync(f => f.Id == id);
			if (feed is null)
			{
				throw new FeedNotFoundException($"Feed with Id {id} not found");
			}
			return feed;
		}

    }
}

