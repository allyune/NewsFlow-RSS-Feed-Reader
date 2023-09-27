using System;
namespace NewsFlow.Application.UseCases.DeleteFeeds
{
	public interface IDeleteFeeds
	{
		public Task DeleteFeed(Guid id);
	}
}

