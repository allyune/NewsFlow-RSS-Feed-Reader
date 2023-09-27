using System;
using NewsFlow.Data.Models;
using NewsFlow.Web.ViewModels;

namespace NewsFlow.Web.Mapping.FeedMapping
{
	public class FeedMapper : IFeedMapper
	{
        public FeedMetadataViewModel FeedMetadataToViewModel(Feed feedModel)
        {
            return FeedMetadataViewModel.Create(
                feedModel.Name, feedModel.Description);
        }
    }
}

