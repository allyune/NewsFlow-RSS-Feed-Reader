using System;
using Microsoft.AspNetCore.Mvc;

namespace NewsFlow.Web.Controllers
{
    [Route("api/[controller]")]
    public class FeedsController : Controller
    {
        private readonly ILogger<FeedsController> _logger;

        public FeedsController(ILogger<FeedsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ListFeeds()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{feedId}")]
        public async Task<IActionResult> LoadFeedDetails(Guid feedId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> AddFeed([FromBody] AddFeedDto data)
        {
            throw new NotImplementedException();
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

