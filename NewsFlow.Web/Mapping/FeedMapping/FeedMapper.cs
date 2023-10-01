using System;
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

        public FeedMetadataViewModel FeedMetadataToViewModel(Feed feedModel)
        {
            var viewModel =  FeedMetadataViewModel.Create(
               feedModel.Id.ToString(), feedModel.Name, feedModel.Description, GetRootDomain(feedModel.Link));
            return viewModel;
        }

        public FeedViewModel FeedToViewModel(Feed feedModel)
        {
            return FeedViewModel.Create(
                feedModel.Name,
                GetRootDomain(feedModel.Link));
        }
    }
}

