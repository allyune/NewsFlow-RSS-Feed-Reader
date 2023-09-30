using System;
using System.ServiceModel.Syndication;
using System.Xml;

namespace NewsFlow.Application.UseCases.Helpers
{
	public class ParseFeedHelpers : IParseFeedHelpers
    {
        public SyndicationFeed GetRss2Feed(XmlReader reader)
        {
            var formatterRss2 = new Rss20FeedFormatter();
            formatterRss2.ReadFrom(reader);
            return formatterRss2.Feed;
        }

        public SyndicationFeed GetAtomFeed(XmlReader reader)
        {
            var formatterAtom = new Atom10FeedFormatter();
            formatterAtom.ReadFrom(reader);
            Console.WriteLine(formatterAtom.Feed);
            return formatterAtom.Feed;
        }
    }
}

