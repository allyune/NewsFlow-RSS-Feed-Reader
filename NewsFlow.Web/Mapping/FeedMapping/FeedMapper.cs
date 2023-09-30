﻿using System;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Xml;
using NewsFlow.Data.Models;
using NewsFlow.Web.ViewModels;

namespace NewsFlow.Web.Mapping.FeedMapping
{
	public class FeedMapper : IFeedMapper
	{
        private string GetRootDomain(string link)
        {
            string urlPattern = @"^(?:https?://)?(?:www\.)?(?<rootDomain>[^/]+)";

            Match match = Regex.Match(
                link, urlPattern, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                return match.Groups["rootDomain"].Value;
            }
            return link;
        }
        private DateTime GetFeedLastUpdated(string feedLink)
        {
            using var reader = XmlReader.Create(feedLink);
            var feed = SyndicationFeed.Load(reader);
            return feed.LastUpdatedTime.DateTime;
        }

        public FeedMetadataViewModel FeedMetadataToViewModel(Feed feedModel)
        {
            Console.WriteLine("ViewModel ID");
            Console.WriteLine(feedModel.Id.ToString());
            var viewModel =  FeedMetadataViewModel.Create(
               feedModel.Id.ToString(), feedModel.Name, feedModel.Description, GetRootDomain(feedModel.Link));
            Console.WriteLine(viewModel.Id);
            return viewModel;
        }

        public FeedViewModel FeedToViewModel(Feed feedModel)
        {
            DateTime lastUpdated = GetFeedLastUpdated(feedModel.Link);
            return FeedViewModel.Create(
                feedModel.Name,
                feedModel.Description,
                GetRootDomain(feedModel.Link),
                lastUpdated);
        }
    }
}

