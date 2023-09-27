namespace RedditRTS.Api.Controllers.ViewModels.Posts
{
    public class PostViewModel
    {
        public string? SubredditNamePrefixed { get; set; }
        public string? Subreddit { get; set; }
        public string? Id { get; set; }
        public string? AuthorName { get; set; }
        public string? AuthorId { get; set; }
        public DateTimeOffset? CreatedUtc { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public int? UpVotes { get; set; }
        public int? DownVotes { get; set; }
        public bool? Hidden { get; set; }
    }
}
