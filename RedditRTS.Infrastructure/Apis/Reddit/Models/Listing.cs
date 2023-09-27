namespace RedditRTS.Infrastructure.Apis.Reddit.Models
{
    public class Listing
    {
        public string? After { get; set; }
        public int? Dist { get; set; }
        public List<Child> Children { get; set; } = new();
        public string? Before { get; set; }
    }
}
