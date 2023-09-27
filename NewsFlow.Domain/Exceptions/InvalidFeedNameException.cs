using System;
namespace NewsFlow.Domain.Exceptions
{
	public class InvalidFeedNameException : Exception
	{
		public InvalidFeedNameException(string message) :base(message)
		{
		}
	}
}

