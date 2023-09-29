using System;
namespace NewsFlow.Web.ViewModels
{
	public class FeedMetadataViewModel
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Link { get; set; }

		private FeedMetadataViewModel(
			string name, string description, string link)
		{
			Name = name;
			Description = description;
			Link = link;
		}

		public static FeedMetadataViewModel Create(
			string name, string description, string link)
		{
			return new FeedMetadataViewModel(name, description, link);

        }

    }
}

