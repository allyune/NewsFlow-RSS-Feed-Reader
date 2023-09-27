using System;
namespace NewsFlow.Application.UseCases.LoadFeeds
{
	public class FeedNotFoundException : Exception
	{
		public FeedNotFoundException(string message) : base(message);
		{
		}
	}
}

