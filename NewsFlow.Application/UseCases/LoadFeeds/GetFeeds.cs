using System;
using System.ServiceModel.Syndication;
using System.Xml;
using Microsoft.Extensions.Logging;
using Models =  NewsFlow.Data.Models;
using NewsFlow.Data.Repositories.FeedRepository;
using NewsFlow.Domain.Entities;
using NewsFlow.Application.Mapping;
using System.Runtime.Serialization;
using NewsFlow.Application.UseCases.Helpers;

namespace NewsFlow.Application.UseCases.LoadFeeds
{
	public class GetFeeds : IGetFeeds
    {
		private readonly IAsyncFeedRepository _feedRepository;
        private readonly IArticleMapper _mapper;
        private readonly ILogger<GetFeeds> _logger;
        private readonly IParseFeedHelpers _parseFeedHelpers;

		public GetFeeds(
			IAsyncFeedRepository repository,
            IArticleMapper mapper,
            ILogger<GetFeeds> logger,
            IParseFeedHelpers helpers)
		{
			_feedRepository = repository;
            _mapper = mapper;
			_logger = logger;
            _parseFeedHelpers = helpers;

        }
        public async Task<List<Models.Feed>> ListFeeds()
		{
			return await _feedRepository.ListAllAsync();
		}
		public async Task<Models.Feed> GetFeed(Guid id)
		{
            Models.Feed? feed = await _feedRepository.GetAsync(f => f.Id == id);
			if (feed is null)
			{
				throw new FeedNotFoundException($"Feed with Id {id} not found");
			}
			return feed;
		}

        public async Task<List<Article>> LoadArticles(Guid feedId)
        {
            Models.Feed? savedFeed = await _feedRepository.GetAsync(f => f.Id == feedId);
            if (savedFeed is null)
            {
                throw new FeedNotFoundException(
                    $"Feed with id {feedId} not found");
            }
            using var reader = XmlReader.Create(savedFeed.Link);
            SyndicationFeed feed;
            try
            {
                feed = _parseFeedHelpers.GetRss2Feed(reader);
            }
            catch (XmlException)
            {
                feed = _parseFeedHelpers.GetAtomFeed(reader);
            }
            var items = feed.Items.ToList();

            var articles = items.Select(
                _mapper.FeedItemToArticle)
                .ToList();

            return articles; 
        }
    }
}

