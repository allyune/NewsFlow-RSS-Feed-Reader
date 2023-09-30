using System;
using NewsFlow.Domain.Entities;
using System.ServiceModel.Syndication;
using HtmlAgilityPack;
using System.Text;

namespace NewsFlow.Application.Mapping
{
    public class ArticleMapper : IArticleMapper
    {
        public Article FeedItemToArticle(SyndicationItem item)
        {
            return Article.Create(
                    item.Id,
                    item.Title.Text,
                    ParseArticleContent(item),
                    item.PublishDate.Date,
                    item.Links.Select(l => l.Uri.AbsoluteUri).ToList(),
                    ParseAuthors(item),
                    item.Categories.Select(c => c.Name).ToList());
        }

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

        private string ParseArticleContent(SyndicationItem item)
        {
            string itemContent;

            if (SummaryExists(item))
            {
                itemContent = item.Summary.Text;
            }
            else if (DescriptionExists(item))
            {
                itemContent = item.ElementExtensions.ReadElementExtensions<string>(
                "description", "http://www.w3.org/2005/Atom")[0];
            }
            else if (ContentExists(item))
            {
                var content = (TextSyndicationContent)item.Content;
                string contentText = content.Text;
                itemContent = MakeArticleSummary(contentText);
            }
            else
            {
                itemContent = " ";
            }
            return itemContent;
        }

        private bool SummaryExists(SyndicationItem item)
        {
            try
            {
                string summaryText = item.Summary.Text;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool DescriptionExists(SyndicationItem item)
        {
            try
            {
                var description = item.ElementExtensions.ReadElementExtensions<string>(
                "description", "http://www.w3.org/2005/Atom");
                if (description.Count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool ContentExists(SyndicationItem item)
        {
            try
            {
                var content = (TextSyndicationContent)item.Content;
                var contentText = content.Text;
                if (contentText.Length > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

       private string MakeArticleSummary(string htmlContent)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlContent);
            var img = htmlDoc.DocumentNode.SelectNodes("//img[position() = 1]")[0];
            var pNodes = htmlDoc.DocumentNode.SelectNodes("//p[position() <= 2]");
            var sb = new StringBuilder();
            if (img != null)
            {
                sb.Append(img.OuterHtml);
                
            }
            if (pNodes != null)
            {
                foreach (var pNode in pNodes)
                {
                    sb.Append(pNode.OuterHtml);
                }
            }
            else
            {
                var words = htmlContent
                        .Split(" ").Take(150).ToArray();
                words[149] = " ...";
                string itemContent = string.Join(" ", words);
                sb.Append(itemContent);
            }

            return sb.ToString();
        }
    }
}