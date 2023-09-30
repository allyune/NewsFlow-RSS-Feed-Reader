using System;
namespace NewsFlow.Application.Mapping
{
	public struct FeedItemElement
	{
		public bool ElementExists { get; set; }
        public string? ElementContents { get; set; }

		private FeedItemElement(bool exists, string? contents)
		{
			ElementExists = exists;
			ElementContents = contents;

        }

		public static FeedItemElement Create(
			bool exists, string? contents = null)
		{
			return new FeedItemElement(exists, contents);

        }
    }
}

