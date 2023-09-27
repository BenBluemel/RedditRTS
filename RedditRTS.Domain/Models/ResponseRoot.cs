using RedditRTS.Domain.Models.Throttling;

namespace RedditRTS.Domain.Models.Reddit
{
    public class ResponseRoot
    {
        public RateLimits? RateLimits { get; set; }
        public string? After { get; set; }
    }
}
