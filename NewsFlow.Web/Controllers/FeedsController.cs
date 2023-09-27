﻿using System;
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

        [HttpGet]
        public async Task<IActionResult> ListFeeds()
        {
            List<Feed> allFeeds = await _getFeeds.ListFeeds();
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
                Feed feed = await _getFeeds.GetFeed(feedId);
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
