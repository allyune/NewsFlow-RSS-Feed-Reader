using System;
using NewsFlow.Data.Models;

namespace NewsFlow.Application.UseCases.LoadFeeds
{
    public interface IGetFeeds
    {
        public Task<List<Feed>> ListFeeds();
        public Task<Feed> GetFeed(Guid id);
    }
}

