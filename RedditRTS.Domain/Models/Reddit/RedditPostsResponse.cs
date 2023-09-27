namespace RedditRTS.Domain.Models.Reddit
{
    public class RedditPostsResponse : ResponseRoot
    {
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
