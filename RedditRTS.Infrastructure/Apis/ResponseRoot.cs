using RedditRTS.Domain.Models.Throttling;

namespace RedditRTS.Api.Infrastructure.Apis
{
    public class ResponseRoot
    {
        public RateLimits? RateLimits { get; set; }
        public string? After { get; set; }
    }
}
