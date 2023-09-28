using System;
using NewsFlow.Data.Models;
using NewsFlow.Web.ViewModels;

namespace NewsFlow.Web.Mapping.FeedMapping
{
	public interface IFeedMapper
	{
        public FeedMetadataViewModel FeedMetadataToViewModel(Feed feedModel);
        public FeedViewModel FeedToViewModel(Feed feedModel);
    }
}

