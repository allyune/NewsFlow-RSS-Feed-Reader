using System;
using System.ServiceModel.Syndication;
using System.Xml;

namespace NewsFlow.Application.UseCases.Helpers
{
	public interface IParseFeedHelpers
	{
        public SyndicationFeed GetRss2Feed(XmlReader reader);
        public SyndicationFeed GetAtomFeed(XmlReader reader);
    }
}

