namespace RedditRTS.Domain.Models.Configuration
{
    public class RedditConfig
    {
        public string? ApiKey { get; set; }
        public string? ApiHost { get; set; }
        public List<string> Subreddits { get; set; } = new();
        public int RateLimitWaitTimeNoHeaders { get; set; }
        public int MaximumRequestLimit { get; set; }
    }
}
