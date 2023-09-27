using System;
using Microsoft.AspNetCore.Mvc;
using NewsFlow.Application.DTOs;
using NewsFlow.Application.UseCases;
using NewsFlow.Application.UseCases.AddFeeds;
using NewsFlow.Application.UseCases.DeleteFeeds;
using NewsFlow.Application.UseCases.LoadFeeds;
using NewsFlow.Domain.Entities;
using NewsFlow.Web.Mapping.FeedMapping;
using NewsFlow.Web.ViewModels;
using Models = NewsFlow.Data.Models;

namespace NewsFlow.Web.Controllers
{
    public class FeedsController : Controller
    {
        private readonly ILogger<FeedsController> _logger;
        private readonly IGetFeeds _getFeeds;
        private readonly IAddFeeds _addFeeds;
        private readonly IDeleteFeeds _deleteFeeds;
        private readonly IFeedMapper _mapper;

        public FeedsController(
            ILogger<FeedsController> logger,
            IGetFeeds getFeeds,
            IAddFeeds addFeeds,
            IDeleteFeeds deleteFeeds,
            IFeedMapper mapper)
        {
            _logger = logger;
            _getFeeds = getFeeds;
            _addFeeds = addFeeds;
            _mapper = mapper;
            _deleteFeeds = deleteFeeds;
        }

        [HttpGet]
        public async Task<IActionResult> ListFeeds()
        {
            List<Models.Feed> allFeeds = await _getFeeds.ListFeeds();
            List<FeedMetadataViewModel> metadata = allFeeds.Select(
                _mapper.FeedMetadataToViewModel).ToList();
            return View(metadata);
        }

        [Route("api/[controller]/{feedId}")]
        [HttpGet]
        public async Task<IActionResult> LoadFeedDetails(Guid feedId)
        {
            try
            {
                Models.Feed feed = await _getFeeds.GetFeed(feedId);
                return Json(feed);
            }
            catch (FeedNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when fetching Feed id {feedId}, {ex.Message}");
                return StatusCode(
                    500, "Error occured when fetching the Feed. Please try again");
            }
            
        }

        [Route("api/[controller]")]
        [HttpPost]
        public async Task<IActionResult> AddFeed([FromBody] AddFeedDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _addFeeds.AddFeed(data);
                return Ok();
            }
            catch (LinkNotUniqueException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning($"Http error when accessing URL {data.Link}: {ex.Message}");
                return BadRequest("Rss Feed URL is unreachanble.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when adding RSS feed {data.Link}: {ex.Message}");
                return StatusCode(
                    500, "Error occured when adding the Feed. Please try again");
            }
        }

        [Route("api/[controller]/{feedId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteFeed(string feedId)
        {
            Guid id;
            bool isGuid = Guid.TryParse(feedId, out id);
            if (!isGuid)
            {
                return BadRequest("Wrong Feed Id format.");
            }
            try
            {
                await _deleteFeeds.DeleteFeed(id);
                return Ok();
            }
            catch (FeedNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when deleting Feed id {id}: {ex.Message}");
                return StatusCode(
                    500, "Error occured when deleting the Feed. Please try again");
            }
        }

        [Route("api/[controller]/{feedId}/articles")]
        [HttpGet]
        public async Task<IActionResult> LoadFeedArticles(string feedId)
        {
            Guid id;
            bool isGuid = Guid.TryParse(feedId, out id);
            if (!isGuid)
            {
                return BadRequest("Wrong Feed Id format.");
            }
            List<Article> artcles = await _getFeeds.LoadArticles(id);
            return Ok(Json(artcles));
        }

    }
}

