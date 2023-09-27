namespace RedditRTS.Domain.Models.Throttling
{
    public class RateLimits
    {
        public double RateLimitRemaining { get; set; }
        public int RateLimitUsed { get; set; }
        public int RateLimitReset { get; set; }
    }
}
