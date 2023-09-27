using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NewsFlow.Application.DTOs
{
	public class AddFeedDto
	{
        [MinLength(1,
			ErrorMessage = "RSS Feed name must contain at least one character"),
		 MaxLength(200,
			ErrorMessage = "RSS Feed name max length is 200 characters"),
		 RegularExpression(@"^[a-zA-Z0-9_]+$",
			ErrorMessage = "Rss Feed name can't contain any special characters")]
        public string Name { get; private set; }

        [RegularExpression(
			@"^(https?://)([\w-]+(\.[\w-]+)+)(/[\w- ./?%&=]*)?$",
			ErrorMessage = "Invalid email, make sure that link starts with http/https://")]
        public string Link { get; private set; }

		public AddFeedDto(string name, string link)
		{
			Name = name;
			Link = link;
		}

    }
}

