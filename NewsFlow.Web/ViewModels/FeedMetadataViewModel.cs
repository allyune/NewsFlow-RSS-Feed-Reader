using System;
namespace NewsFlow.Web.ViewModels
{
	public class FeedMetadataViewModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Link { get; set; }

		private FeedMetadataViewModel(
			string id, string name, string description, string link)
		{
			Id = id;
			Name = name;
			Description = description;
			Link = link;
		}

		public static FeedMetadataViewModel Create(
			string id, string name, string description, string link)
		{
			return new FeedMetadataViewModel(id, name, description, link);

        }

    }
}

