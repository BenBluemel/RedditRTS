using RedditRTS.Domain.Models.Throttling;

namespace RedditRTS.Api.Domain.Interfaces
{
    public interface IThrottleStrategy
    {
        public TimeSpan TimeToWait(RateLimits? rateLimits);
    }
}