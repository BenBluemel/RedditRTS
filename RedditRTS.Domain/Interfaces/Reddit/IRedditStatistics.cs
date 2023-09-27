using RedditRTS.Domain.Models.Reddit;

namespace RedditRTS.Api.Domain.Interfaces.Reddit
{
    public interface IRedditStatistics
    {
        Task<List<AuthorWithMostPosts>> GetAuthorsWithMostPostsAsync(string? subreddit, int limit = 10);
        Task<List<Post>> GetPostsWithMostUpvotesAsync(string? subreddit, int limit = 10);
    }

    public interface IRedditPersistence
    {
        Task<Post> InsertPostAsync(Post post);
    }
}