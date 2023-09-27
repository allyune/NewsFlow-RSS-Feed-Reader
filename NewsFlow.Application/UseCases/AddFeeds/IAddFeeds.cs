using System;

using NewsFlow.Application.DTOs;

namespace NewsFlow.Application.UseCases.AddFeeds
{
	public interface IAddFeeds
	{
		Task AddFeed(AddFeedDto data);
	}
}

