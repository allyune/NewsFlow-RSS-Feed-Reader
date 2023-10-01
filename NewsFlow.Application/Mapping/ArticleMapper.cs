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
                    item.PublishDate.DateTime,
                    item.Links.Select(l => l.Uri.AbsoluteUri).ToList(),
                    ParseAuthors(item),
                    item.Categories.Select(c => c.Name).ToList());
        }

        /// <summary>
        /// Trying to parse authors from different RSS schemas.
        /// 1. RSS2.0 author with name
        /// 2. RSS2.0 author with email
        /// 3. Atom creator
        /// 4. Other author field.
        /// Returns list with "Author" as default value.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private List<string> ParseAuthors(SyndicationItem item)
        {
            var authorsWithName = item.Authors.Select(a => a.Name);
            if (authorsWithName.Count() > 0 &&
                !(authorsWithName.First() is null))
            {
                return authorsWithName.ToList();
            }
            var authorsWithEmail = item.Authors.Select(a => a.Email);
            if (authorsWithEmail.Count() > 0 &&
                !(authorsWithEmail.First() is null))
            {
                return authorsWithEmail.ToList();
            }
            var dcCreators = item.ElementExtensions.ReadElementExtensions<string>(
                "creator", "http://purl.org/dc/elements/1.1/");
            if (dcCreators.Count() > 0 &&
                !(dcCreators.First() is null))
            {
                return dcCreators.ToList();
            }
            var authorsCustom = item.ElementExtensions.ReadElementExtensions<string>(
                "author", "");
            if (authorsCustom.Count() > 0)
            {
                return authorsCustom.ToList();
            }

            return new List<string>() { "author" };
        }

        /// <summary>
        /// Tries to parse article summary from different RSS schema types.
        /// If summary is not found it is constructed from content.
        /// Returns empty string as default value.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string ParseArticleContent(SyndicationItem item)
        {

            FeedItemElement summary = TryGetSummary(item);
            if (summary.ElementExists)
            {
                return summary.ElementContents;
            }

            FeedItemElement description = TryGetDescription(item);
            if (description.ElementExists)
            {
                return description.ElementContents;
            }

            FeedItemElement content = TryGetContent(item);
            if (content.ElementExists)
            {
                return content.ElementContents;
            }

            else
            {
                return " ";
            }
        }

        private FeedItemElement TryGetSummary(SyndicationItem item)
        {
            try
            {
                return FeedItemElement.Create(
                    true, item.Summary.Text);
            }
            catch (Exception)
            {
                return FeedItemElement.Create(false);
            }
        }
        private FeedItemElement TryGetDescription(SyndicationItem item)
        {
            try
            {
                var description = item.ElementExtensions.ReadElementExtensions<string>(
                "description", "http://www.w3.org/2005/Atom");
                if (description.Count > 0)
                {
                    string content = item.ElementExtensions.ReadElementExtensions<string>(
                        "description", "http://www.w3.org/2005/Atom")[0];
                    return FeedItemElement.Create(true, content);
                }
                return FeedItemElement.Create(false);
            }
            catch (Exception)
            {
                return FeedItemElement.Create(false);
            }
        }

        private FeedItemElement TryGetContent(SyndicationItem item)
        {
            try
            {
                var content = (TextSyndicationContent)item.Content;
                var contentText = content.Text;
                var summary = MakeArticleSummary(contentText);
                if (contentText.Length > 0)
                {
                    return FeedItemElement.Create(true, summary);
                }
                return FeedItemElement.Create(false);
            }
            catch (Exception)
            {
                return FeedItemElement.Create(false);
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