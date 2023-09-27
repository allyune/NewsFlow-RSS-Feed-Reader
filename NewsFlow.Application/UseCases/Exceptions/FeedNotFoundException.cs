using System;
namespace NewsFlow.Application.UseCases
{
    public class FeedNotFoundException : Exception
    {
        public FeedNotFoundException(string message) : base(message)
        {
        }
    }
}



