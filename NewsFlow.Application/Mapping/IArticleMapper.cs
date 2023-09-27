using System;
using NewsFlow.Domain.Entities;
using System.ServiceModel.Syndication;

namespace NewsFlow.Application.Mapping
{
    public class ArticleMapper : IArticleMapper
    {
        public Article FeedItemToArticle(SyndicationItem item)
        {
            return Article.Create(
                    item.Id,
                    item.Title.Text,
                    item.Summary.Text,
                    item.PublishDate.Date,
                    item.Links.Select(l => l.Uri.AbsoluteUri).ToList(),
                    item.Authors.Select(a => a.Name).ToList(),
                    item.Categories.Select(c => c.Name).ToList());
        }
    }
}

