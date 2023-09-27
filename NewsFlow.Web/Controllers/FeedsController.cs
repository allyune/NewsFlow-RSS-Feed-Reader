using System;
using Microsoft.AspNetCore.Mvc;
using NewsFlow.Application.DTOs;
using NewsFlow.Application.UseCases.AddFeeds;
using NewsFlow.Application.UseCases.LoadFeeds;
using NewsFlow.Data.Models;
using NewsFlow.Web.Mapping.FeedMapping;
using NewsFlow.Web.ViewModels;

namespace NewsFlow.Web.Controllers
{
    public class FeedsController : Controller
    {
        private readonly ILogger<FeedsController> _logger;
        private readonly IGetFeeds _getFeeds;
        private readonly IAddFeeds _addFeeds;
        private readonly IFeedMapper _mapper;

        public FeedsController(
            ILogger<FeedsController> logger,
            IGetFeeds getFeeds,
            IAddFeeds addFeeds,
            IFeedMapper mapper)
        {
            _logger = logger;
            _getFeeds = getFeeds;
            _addFeeds = addFeeds;
            _mapper = mapper;
        }

        // Todo: error handling
        [HttpGet]
        public async Task<IActionResult> ListFeeds()
        {
            List<Feed> allFeeds = await _getFeeds.ListFeeds();
            List<FeedMetadataViewModel> metadata = allFeeds.Select(
                _mapper.FeedMetadataToViewModel).ToList();
            return View(metadata);
        }

        [HttpGet("{feedId}")]
        public async Task<IActionResult> LoadFeedDetails(Guid feedId)
        {
            throw new NotImplementedException();
        }

        //Todo: Check if link is unique by stripping it off http/wwww
        [Route("api/[controller]")]
        [HttpPost]
        public async Task<IActionResult> AddFeed([FromBody] AddFeedDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _addFeeds.AddFeed(data);
            return Ok();
        }

        [HttpDelete("{feedId}")]
        public async Task<IActionResult> DeleteFeed(Guid feedId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{feedId}")]
        public async Task<IActionResult> LoadFeedArticles(Guid feedId)
        {
            throw new NotImplementedException();
        }

    }
}

