using System;
using Microsoft.AspNetCore.Mvc;
using NewsFlow.Application.UseCases.AddFeeds;
using NewsFlow.Application.UseCases.DeleteFeeds;
using NewsFlow.Application.UseCases.LoadFeeds;
using NewsFlow.Web.Controllers;
using NewsFlow.Web.Mapping.FeedMapping;
using NewsFlow.Web.ViewModels;
using Models = NewsFlow.Data.Models;

namespace NewsFlow.Web.ViewComponents
{
	public class ListFeedsViewComponent : ViewComponent
	{
        private readonly IGetFeeds _getFeeds;
        private readonly IFeedMapper _mapper;

        public ListFeedsViewComponent(
            IGetFeeds getFeeds,
            IFeedMapper mapper)
        {
            _getFeeds = getFeeds;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            List<Models.Feed> allFeeds = await _getFeeds.ListFeeds();
            List<FeedMetadataViewModel> metadata = allFeeds.Select(
                _mapper.FeedMetadataToViewModel).ToList();
            return View(metadata);
        }
    }
}

