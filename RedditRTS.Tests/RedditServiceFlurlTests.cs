using AutoMapper;
using FluentAssertions;

using Flurl.Http.Testing;

using Microsoft.Extensions.Options;
using Moq;
using RedditRTS.Domain.Models.Configuration;
using RedditRTS.Domain.Models.Reddit;
using RedditRTS.Infrastructure.Apis.Reddit;
using RedditRTS.Infrastructure.Apis.Reddit.Models;

namespace RedditRTS.Infrastructure.Tests
{
    public class RedditServiceFlurlTests
    {
        [Fact]
        public async Task RedditServiceFlurl_NoHeaders_Return_Null_RateLimits()
        {
            // Arrange
            using var httpTest = new HttpTest();

            httpTest.RespondWithJson(NoPostResponse);
            var redditConfigMock = new Mock<IOptions<RedditConfig>>();
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<RedditPostsResponse>(It.IsAny<object>()))
                .Returns(new RedditPostsResponse());
            
            redditConfigMock.Setup(x => x.Value)
                .Returns(new RedditConfig
                {
                    ApiKey = "test_me",
                    ApiHost = "https://oauth.reddit.com",
                    Subreddits = new List<string> { "r/test" }
                });

            var service = new RedditServiceFlurl(redditConfigMock.Object, mapperMock.Object);
            
            // Act
            var result = await service.GetNextPostsAsync(redditConfigMock.Object.Value.Subreddits.First(), null);
            
            // Assert
            result.Should().NotBeNull();
            result.RateLimits.Should().BeNull();
        }

        public Root NoPostResponse = new Root
        {
            Data = new Listing()
        };
    }
}