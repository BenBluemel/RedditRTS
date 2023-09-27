using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using RedditRTS.Api.Domain.Interfaces;
using RedditRTS.Domain.Models.Configuration;
using RedditRTS.Domain.Models.Throttling;

using System.Diagnostics;

namespace RedditRTS.Api.Infrastructure.Apis.Throttling
{
    public class SmoothThrottle : IThrottleStrategy
    {
        private readonly ILogger<SmoothThrottle> _logger;
        private readonly RedditConfig _redditConfig;

        public SmoothThrottle(ILogger<SmoothThrottle> logger, IOptions<RedditConfig> redditConfig)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _redditConfig = redditConfig?.Value ?? throw new ArgumentNullException(nameof(redditConfig));
        }
        public TimeSpan TimeToWait(RateLimits? rateLimits)
        {
            if (rateLimits == null)
            {
                // Todo: Default wait time
                return TimeSpan.FromMilliseconds(_redditConfig.RateLimitWaitTimeNoHeaders);
            }

            _logger.LogInformation("rateLimitingRemaining {rlr}, rateLimitUsed {rlu}, rateLimitReset {rlreset}",
                rateLimits.RateLimitRemaining,
                rateLimits.RateLimitUsed,
                rateLimits.RateLimitReset);
            var waitTime = CalculateWaitTime(rateLimits);
            Debug.WriteLine($"waitTime: {waitTime}");

            return waitTime;
        }

        private TimeSpan CalculateWaitTime(RateLimits rateLimits)
        {
            return rateLimits.RateLimitRemaining == 1
                ? TimeSpan.FromSeconds(rateLimits.RateLimitReset)
                : TimeSpan.FromSeconds(rateLimits.RateLimitReset / rateLimits.RateLimitRemaining);
        }
    }
}
