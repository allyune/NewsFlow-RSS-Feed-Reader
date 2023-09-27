using System;
namespace NewsFlow.Application.UseCases.AddFeeds
{
	public class LinkNotUniqueException : Exception 
	{
		public LinkNotUniqueException(string message) :base(message)
		{
		}
	}
}

