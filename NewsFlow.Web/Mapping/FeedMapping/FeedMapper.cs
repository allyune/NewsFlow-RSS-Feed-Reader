using System;
using System.Text.RegularExpressions;
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
            return FeedMetadataViewModel.Create(
                feedModel.Name, feedModel.Description, GetRootDomain(feedModel.Link));
        }
    }
}

