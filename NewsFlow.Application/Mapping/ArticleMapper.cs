using System;
using System.ServiceModel.Syndication;
using NewsFlow.Domain.Entities;

namespace NewsFlow.Application.Mapping
{
	public interface IArticleMapper
    {
		public Article FeedItemToArticle(SyndicationItem item);
	}
}