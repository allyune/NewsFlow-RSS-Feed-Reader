using System;
namespace NewsFlow.Domain.Exceptions
{
	public class InvalidFeedLinkException : Exception
	{
		public InvalidFeedLinkException(string message) :base(message)
		{
		}
	}
}

