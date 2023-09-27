using System;
using Microsoft.VisualBasic;
using NewsFlow.Domain.Base;

namespace NewsFlow.Domain.Entities
{
    public class Article : BaseEntity
    {
        public string Title { get; private set; }
        public string Summary { get; private set; }
        public DateTime PubDate { get; private set; }
        public string Link { get; private set; }
        public List<string> Authors { get; private set; }
        public List<string> Categories { get; set; }

        public Article(
            string title,
            string summary,
            DateTime pubDate,
            string link, List<string> authors,
            List<string> categories)
        {
            Title = title;
            Summary = summary;
            PubDate = pubDate;
            Link = link;
            Authors = authors;
            Categories = categories;
        }

        public static Article Create(
            string title,
            string summary,
            DateTime pubDate,
            string link,
            List<string> authors,
            List<string> categories)
        {
            return new Article(
                title, summary, pubDate, link, authors, categories);
        }
    }
}

