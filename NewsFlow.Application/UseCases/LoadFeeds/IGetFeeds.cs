using System;
using NewsFlow.Application.DTOs;
using NewsFlow.Data.Models;
using NewsFlow.Domain.Entities;
using Models = NewsFlow.Data.Models;

namespace NewsFlow.Application.UseCases.LoadFeeds
{
    public interface IGetFeeds
    {
        public Task<List<Models.Feed>> ListFeeds();
        public Task<Models.Feed> GetFeed(Guid id);
        public Task<ReadFeedDto> LoadArticles(Guid feedId);
    }
}

