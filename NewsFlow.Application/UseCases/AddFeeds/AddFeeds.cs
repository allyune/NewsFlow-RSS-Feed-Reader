using System;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using NewsFlow.Application.DTOs;
using NewsFlow.Application.UseCases.Helpers;
using NewsFlow.Data.Repositories.FeedRepository;
using NewsFlow.Domain.Entities;
using Models = NewsFlow.Data.Models;

namespace NewsFlow.Application.UseCases.AddFeeds
{
	public class AddFeeds : IAddFeeds
	{
        private readonly IAsyncFeedRepository _feedRepository;
        private readonly ILogger<AddFeeds> _logger;
        private readonly IParseFeedHelpers _parseFeedHelpers;

        public AddFeeds(
            IAsyncFeedRepository repository,
            ILogger<AddFeeds> logger,
            IParseFeedHelpers helpers)
        {
            _feedRepository = repository;
            _logger = logger;
            _parseFeedHelpers = helpers;
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
            SyndicationFeed feed;
            try
            {
                feed = _parseFeedHelpers.GetRss2Feed(reader);
            }
            catch(XmlException)
            {
                feed = _parseFeedHelpers.GetAtomFeed(reader);
            }

            string description = GetFeedDescription(link, feed);
            Feed newFeed = Feed.Create(name, link, description);
            Models.Feed feedModel = Models.Feed.Create(
                newFeed.Id, newFeed.Name, newFeed.Link, newFeed.Description);
            await _feedRepository.AddAsync(feedModel);
            await _feedRepository.SaveChangesAsync();
            }

        private string GetFeedDescription(string link, SyndicationFeed feed)
        {
            string description;
            try
            {
                description = feed.Description.Text;
            }
            // Desceription is not found in the feed
            catch (NullReferenceException)
            {
                // Default feed description
                description = $"RSS Feed from {CleanUrl(link)}";
            }

            return description;
        }

    }
    }

