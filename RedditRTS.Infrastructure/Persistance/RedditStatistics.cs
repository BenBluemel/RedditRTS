using RedditRTS.Domain.Interfaces.Persistence;
using RedditRTS.Domain.Models.Reddit;

namespace RedditRTS.Api.Infrastructure.Persistance
{
    public class RedditStatistics : IRedditStatistics
    {
        public Task<List<AuthorWithMostPosts>> GetAuthorsWithMostPostsAsync(string? subreddit, int limit = 10)
        {
            return Task.FromResult(RedditMemoryStorage.Posts
                .Where(x => subreddit == null || string.Equals(x.Value.Subreddit, subreddit, StringComparison.OrdinalIgnoreCase))
                .GroupBy(x => x.Value.AuthorId).OrderByDescending(g => g.Count())
                .Select(g => new AuthorWithMostPosts
                {
                    AuthorName = g.First().Value.AuthorName,
                    AuthorId = g.First().Value.AuthorId,
                    NumberOfPosts = g.Count(),
                    Posts = g.Select(x => x.Value).ToList()
                }).Take(limit).ToList());
        }

        public Task<List<Post>> GetPostsWithMostUpvotesAsync(string? subreddit, int limit = 10)
        {
            return Task.FromResult(RedditMemoryStorage.Posts.Where(x => subreddit == null || string.Equals(x.Value.Subreddit, subreddit, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.Value.UpVotes).Select(g => g.Value).Take(limit).ToList());
        }
    }
}
