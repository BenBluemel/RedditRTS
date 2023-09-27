namespace RedditRTS.Api.Controllers.ViewModels.Posts
{
    public class AuthorViewModel
    {
        public string? AuthorName { get; set; }
        public string? AuthorId { get; set; }
        public int NumberOfPosts { get; set; }
        public List<PostViewModel> Posts { get; set; } = new List<PostViewModel>();
    }
}
