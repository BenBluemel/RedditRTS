using Microsoft.Extensions.Logging;
using RedditRTS.Api.Domain.Interfaces.Reddit;
using RedditRTS.Api.Domain.Interfaces;
using RedditRTS.Api.Infrastructure.Apis.Reddit;
using RedditRTS.Domain.Interfaces.Persistence;
using RedditRTS.Domain.Models.Configuration;
using Moq;
using Microsoft.Extensions.Options;
using RedditRTS.Domain.Models.Reddit;

namespace RedditRTS.Tests.Domain
{
    public class MultiSubredditRoundRobinRedditSupervisorTests
    {
        [Fact]
        public async Task CollectDataAsync_Calls_GetNextPost_For_All_Subreddits()
        {
            var redditServiceMock = new Mock<IRedditService>();
            var redditPersistenceMock = new Mock<IRedditPersistence>();
            var throttleStrategyMock = new Mock<IThrottleStrategy>();
            var redditConfig = new Mock<IOptions<RedditConfig>>();
            var loggerMock = new Mock<ILogger<MultiSubredditRoundRobinRedditSupervisor>>();

            var subreddits = new List<string>
            {
                "r/abc",
                "r/bcd",
                "r/nothing",
                "r/whatever"
            };

            redditConfig.Setup(x => x.Value)
                .Returns(new RedditConfig
                {
                    Subreddits = subreddits
                });

            redditServiceMock.Setup(x => x.GetNextPostsAsync(It.IsAny<string>(), It.IsAny<string?>()))
                .ReturnsAsync(new RedditPostsResponse
                {
                    After = null,
                    Posts = new List<Post>(),
                    RateLimits = null
                });

            var cut = new MultiSubredditRoundRobinRedditSupervisor(
                redditServiceMock.Object,
                redditPersistenceMock.Object,
                throttleStrategyMock.Object,
                redditConfig.Object,
                loggerMock.Object);

            await cut.CollectDataAsync();

            foreach (var subreddit in subreddits)
            {
                redditServiceMock.Verify(x => x.GetNextPostsAsync(subreddit, null), Times.Once());
            }
        }
    }
}
