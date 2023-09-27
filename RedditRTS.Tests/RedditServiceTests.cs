using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using RedditRTS.Domain.Models.Configuration;
using RedditRTS.Infrastructure.Apis.Reddit;

namespace RedditRTS.Infrastructure.Tests
{
    public class RedditServiceTests
    {
        [Fact]
        public async Task Test()
        {
            var redditConfigMock = new Mock<IOptions<RedditConfig>>();
            var mapperMock = new Mock<IMapper>();
            redditConfigMock.Setup(x => x.Value)
                .Returns(new RedditConfig
                {
                    ApiKey = "test_me",
                    ApiHost = "https://oauth.reddit.com",
                    Subreddits = new List<string> { "r/test" }
                });

            var service = new RedditServiceFlurl(redditConfigMock.Object, mapperMock.Object);
            var result = await service.GetNextPostsAsync(redditConfigMock.Object.Value.Subreddits.First(), null);
            result.Should().NotBeNull();
        }
    }
}