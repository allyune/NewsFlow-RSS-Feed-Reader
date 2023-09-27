using System;
namespace NewsFlow.Application.UseCases
{
	public class LinkNotUniqueException : Exception 
	{
		public LinkNotUniqueException(string message) :base(message)
		{
		}
	}
}

