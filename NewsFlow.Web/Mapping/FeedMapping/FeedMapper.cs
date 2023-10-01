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
        private DateTime? GetFeedLastUpdated(string feedLink)
        {
            using var reader = XmlReader.Create(feedLink);
            var feed = SyndicationFeed.Load(reader);
            try
            {
                var date = feed.LastUpdatedTime.DateTime;
                //when last updated is not present,
                //invalid date is occasionally produced (e.g. 01/01/0001).
                // checking whether date makes sense.
                if (date.Year > DateTime.Now.Year - 20)
                {
                    return date;
                }
                else
                {
                    return feed.Items.OrderByDescending(
                        i => i.PublishDate).First().PublishDate.DateTime;
                }
            }
            catch (XmlException)
            {
                return null;
            }
        }

        public FeedMetadataViewModel FeedMetadataToViewModel(Feed feedModel)
        {
            var viewModel =  FeedMetadataViewModel.Create(
               feedModel.Id.ToString(), feedModel.Name, feedModel.Description, GetRootDomain(feedModel.Link));
            return viewModel;
        }

        public FeedViewModel FeedToViewModel(Feed feedModel)
        {
            DateTime? lastUpdated = GetFeedLastUpdated(feedModel.Link);
            return FeedViewModel.Create(
                feedModel.Name,
                feedModel.Description,
                GetRootDomain(feedModel.Link),
                lastUpdated);
        }
    }
}

