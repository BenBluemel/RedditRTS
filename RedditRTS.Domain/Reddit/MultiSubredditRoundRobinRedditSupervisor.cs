using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RedditRTS.Api.Domain.Interfaces;
using RedditRTS.Api.Domain.Interfaces.Reddit;
using RedditRTS.Domain.Interfaces.Persistence;
using RedditRTS.Domain.Models.Configuration;
using RedditRTS.Domain.Models.Reddit;

namespace RedditRTS.Api.Infrastructure.Apis.Reddit
{
    public class MultiSubredditRoundRobinRedditSupervisor : IRedditSupervisor
    {
        private readonly IRedditService _redditService;
        private readonly IRedditPersistence _redditPersistence;
        private readonly IThrottleStrategy _throttleStrategy;
        private readonly ILogger<MultiSubredditRoundRobinRedditSupervisor> _logger;
        private readonly RedditConfig _redditConfig;
        private readonly List<string> _subreddits = new();

        private static Dictionary<string, string?> _latestPost = new();

        public MultiSubredditRoundRobinRedditSupervisor(IRedditService redditService, IRedditPersistence redditPersistence,
            IThrottleStrategy throttleStrategy, IOptions<RedditConfig> redditConfig, ILogger<MultiSubredditRoundRobinRedditSupervisor> logger)
        {
            _redditService = redditService ?? throw new ArgumentNullException(nameof(redditService));
            _redditConfig = redditConfig?.Value ?? throw new ArgumentNullException(nameof(redditConfig));
            _redditPersistence = redditPersistence ?? throw new ArgumentNullException(nameof(redditPersistence));
            _throttleStrategy = throttleStrategy ?? throw new ArgumentNullException(nameof(throttleStrategy));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _subreddits.AddRange(_redditConfig.Subreddits ?? throw new ArgumentOutOfRangeException(nameof(redditConfig), $"{nameof(_redditConfig.Subreddits)} config is null"));
            if (!_subreddits.Any())
            {
                throw new ArgumentOutOfRangeException(nameof(redditConfig), "No subreddits defined in config");
            }

            foreach (var subreddit in _redditConfig.Subreddits)
            {
                _latestPost.Add(subreddit, null);
            }
        }
        public async Task CollectDataAsync()
        {
            foreach (var subreddit in _subreddits)
            {
                try
                {
                    var nextPosts = await _redditService.GetNextPostsAsync(subreddit, _latestPost[subreddit]);
                    _latestPost[subreddit] = nextPosts.After ?? nextPosts.Posts.LastOrDefault()?.Id ?? _latestPost[subreddit];
                    var persistPosts = PersistPostsAsync(subreddit, nextPosts);
                    var throttleDelay = Task.Delay((int)_throttleStrategy.TimeToWait(nextPosts?.RateLimits).TotalMilliseconds);
                    await Task.WhenAll(persistPosts, throttleDelay);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Exception while getting data for subreddit {subreddit} latestPost {latestPost}", subreddit, _latestPost[subreddit]);
                    continue;
                }
            }
        }

        private async Task PersistPostsAsync(string subreddit, RedditPostsResponse nextPosts)
        {
            foreach (var post in nextPosts.Posts)
            {
                if (post == null)
                {
                    _logger.LogWarning("Post returned null in {subreddit}, after {after}", subreddit, nextPosts?.After);
                    continue;
                }
                await _redditPersistence.InsertPostAsync(post);
            }
        }
    }
}
