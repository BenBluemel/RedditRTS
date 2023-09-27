using RedditRTS.Api.Domain.Interfaces.Reddit;
using RedditRTS.Domain.Models.Reddit;

namespace RedditRTS.Api.Infrastructure.Persistance
{
    public class RedditStatistics : IRedditStatistics
    {
        public Task<IEnumerable<string>> GetAuthorsWithMostPostsAsync(string? subreddit, int limit = 10)
        {
            return Task.FromResult(RedditMemoryStorage.Posts.Where(x => subreddit == null || string.Equals(x.Value.Subreddit, subreddit, StringComparison.OrdinalIgnoreCase))
                .GroupBy(x => x.Value.AuthorId).OrderByDescending(g => g.Count()).Select(g => g.First().Value.AuthorId).Take(limit));
        }

        public Task<IEnumerable<Post>> GetPostsWithMostUpvotesAsync(string? subreddit, int limit = 10)
        {
            return Task.FromResult(RedditMemoryStorage.Posts.Where(x => subreddit == null || string.Equals(x.Value.Subreddit, subreddit, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.Value.UpVotes).Select(g => g.Value).Take(limit));
        }
    }
}
