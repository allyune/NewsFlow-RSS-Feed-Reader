using System;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Extensions.Logging;
using NewsFlow.Application.DTOs;
using NewsFlow.Data.Repositories.FeedRepository;
using NewsFlow.Domain.Entities;
using Models = NewsFlow.Data.Models;

namespace NewsFlow.Application.UseCases.AddFeeds
{
	public class AddFeeds : IAddFeeds
	{
        private readonly IAsyncFeedRepository _feedRepository;
        private readonly ILogger<AddFeeds> _logger;

        public AddFeeds(
            IAsyncFeedRepository repository,
            ILogger<AddFeeds> logger)
        {
            _feedRepository = repository;
            _logger = logger;
        }

        private string CleanUrl(string url)
        {
            string pattern = @"^(https?://)?(www\.)?|\/$";
            return Regex.Replace(url, pattern, string.Empty);
            
        }

        private async Task<bool> CheckLinkExists(string link)
        {
        string cleanLink = CleanUrl(link);
           return await _feedRepository.CheckExists(
               f => f.Link.Contains(cleanLink.ToLower()));
        }


    public async Task AddFeed(AddFeedDto data)
        {
            string name = data.Name;
            string link = data.Link.ToLower();
            bool isLinkNotUnique = await CheckLinkExists(link);
            if(isLinkNotUnique)
            {
                throw new LinkNotUniqueException(
                    "RSS Feed with this URL has already been added");
            }
            using var reader = XmlReader.Create(link);
            var feed = SyndicationFeed.Load(reader);
            string description = feed.Description.Text;
            Feed newFeed = Feed.Create(name, link, description);
            Models.Feed feedModel = Models.Feed.Create(
                newFeed.Id, newFeed.Name, newFeed.Link, newFeed.Description);
            await _feedRepository.AddAsync(feedModel);
            await _feedRepository.SaveChangesAsync();
            }
        }
    }
}

