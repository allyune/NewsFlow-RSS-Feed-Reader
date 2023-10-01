using System;
using Microsoft.VisualBasic;
using NewsFlow.Domain.Base;

namespace NewsFlow.Domain.Entities
{
    public class Article : BaseEntity
    {
        public string ArticleId { get; private set; }
        public string Title { get; private set; }
        public string Summary { get; private set; }
        public DateTime PubDate { get; private set; }
        public List<string> Links { get; private set; }
        public List<string> Authors { get; private set; }
        public List<string> Categories { get; private set; }

        public Article(
            Guid id,
            string articleId,
            string title,
            string summary,
            DateTime pubDate,
            List<string> links,
            List<string> authors,
            List<string> categories)
        {
            Id = id;
            ArticleId = articleId;
            Title = title;
            Summary = summary;
            PubDate = pubDate;
            Links = links;
            Authors = authors;
            Categories = categories;
        }

        public static Article Create(
            string articleId,
            string title,
            string summary,
            DateTime pubDate,
            List<string> links,
            List<string> authors,
            List<string> categories,
            Guid id = new Guid())
        {
            return new Article(
               id, articleId, title, summary, pubDate, links, authors, categories);
        }
    }
}

