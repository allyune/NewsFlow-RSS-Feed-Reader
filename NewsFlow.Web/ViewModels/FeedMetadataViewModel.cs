using System;
namespace NewsFlow.Web.ViewModels
{
	public class FeedMetadataViewModel
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public FeedMetadataViewModel(string name, string description)
		{
			Name = name;
			Description = description;
		}
	}
}

