using System;
using NewsFlow.Domain.Entities;

namespace NewsFlow.Application.DTOs
{
    public class ReadFeedDto
    {
    public List<Article> Articles { get; private set; }
    public DateTime? LastUpdated { get; private set; }
    
    private ReadFeedDto(
        List<Article> articles, DateTime? lastUpdated)
    {
        Articles = articles;
        LastUpdated = lastUpdated;
	}

    public static ReadFeedDto Create(
        List<Article> articles, DateTime? lastUpdated)
    {
        return new ReadFeedDto(articles, lastUpdated);
    }

	}
}

