using System;
namespace NewsFlow.Web.ViewModels
{
	public class FeedViewModel
	{
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime LastUpdated { get; set; }

        private FeedViewModel(
            string name, string description, string link, DateTime lastUpdated)
        {
            Name = name;
            Description = description;
            Link = link;
            LastUpdated = lastUpdated;
        }

        public static FeedViewModel Create(
            string name, string description, string link, DateTime lastUpdated)
        {
            return new FeedViewModel(name, description, link, lastUpdated);

        }
    }
}

