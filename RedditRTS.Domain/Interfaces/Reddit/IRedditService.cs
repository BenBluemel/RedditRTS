using RedditRTS.Domain.Models.Reddit;

namespace RedditRTS.Api.Domain.Interfaces.Reddit
{
    public interface IRedditService
    {
        Task<RedditPostsResponse> GetNextPostsAsync(string subreddit, string? after);
    }
}