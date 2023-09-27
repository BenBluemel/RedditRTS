using RedditRTS.Domain.Models.Reddit;

namespace RedditRTS.Domain.Interfaces.Persistence
{
    public interface IRedditStatistics
    {
        Task<List<AuthorWithMostPosts>> GetAuthorsWithMostPostsAsync(string? subreddit, int limit = 10);
        Task<List<Post>> GetPostsWithMostUpvotesAsync(string? subreddit, int limit = 10);
    }
}