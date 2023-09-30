using System;
using NewsFlow.Data.Models;
using NewsFlow.Domain.Entities;
using Models = NewsFlow.Data.Models;

namespace NewsFlow.Application.UseCases.LoadFeeds
{
    public interface IGetFeeds
    {
        public Task<List<Models.Feed>> ListFeeds();
        public Task<Models.Feed> GetFeed(Guid id);
        public Task<List<Article>> LoadArticles(Guid feedId);
    }
}

