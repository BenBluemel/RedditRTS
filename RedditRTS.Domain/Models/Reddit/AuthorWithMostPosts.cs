namespace RedditRTS.Domain.Models.Reddit
{
    public class AuthorWithMostPosts
    {
        public string? AuthorName { get; set; }
        public string? AuthorId { get; set; }
        public int NumberOfPosts { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}