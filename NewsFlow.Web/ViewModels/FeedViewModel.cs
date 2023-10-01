using System;
namespace NewsFlow.Web.ViewModels
{
	public class FeedViewModel
	{
        public string Name { get; set; }
        public string Link { get; set; }

        private FeedViewModel(
            string name, string link)
        {
            Name = name;
            Link = link;
        }

        public static FeedViewModel Create(
            string name, string link)
        {
            return new FeedViewModel(name, link);

        }
    }
}

