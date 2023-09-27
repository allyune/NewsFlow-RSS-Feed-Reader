using System;
using NewsFlow.Domain.Entities;
using System.ServiceModel.Syndication;

namespace NewsFlow.Application.Mapping
{
    public class ArticleMapper : IArticleMapper
    {
        private List<string> ParseAuthors(SyndicationItem item)
        {
            var authorsDefault = item.Authors.Select(a => a.Name);
            if (authorsDefault.Count() > 0)
            {
                return authorsDefault.ToList();
            }
            var dcCreators = item.ElementExtensions.ReadElementExtensions<string>(
                "creator", "http://purl.org/dc/elements/1.1/");
            if (dcCreators.Count() > 0)
            {
                return dcCreators.ToList();
            }

            return new List<string>();
        }

        public Article FeedItemToArticle(SyndicationItem item)
        {
            return Article.Create(
                    item.Id,
                    item.Title.Text,
                    item.Summary.Text,
                    item.PublishDate.Date,
                    item.Links.Select(l => l.Uri.AbsoluteUri).ToList(),
                    ParseAuthors(item),
                    item.Categories.Select(c => c.Name).ToList());
        }
    }
}