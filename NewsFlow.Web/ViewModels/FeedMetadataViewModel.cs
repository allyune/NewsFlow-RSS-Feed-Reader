using System;
namespace NewsFlow.Web.ViewModels
{
	public class FeedMetadataViewModel
	{
		public string Name { get; set; }
		public string Description { get; set; }

		private FeedMetadataViewModel(string name, string description)
		{
			Name = name;
			Description = description;
		}

		public static FeedMetadataViewModel Create(string name, string description)
		{
			return new FeedMetadataViewModel(name, description);

        }

    }
}

