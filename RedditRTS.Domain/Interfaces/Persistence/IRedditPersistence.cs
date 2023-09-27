using RedditRTS.Domain.Models.Reddit;

namespace RedditRTS.Domain.Interfaces.Persistence
{
    public interface IRedditPersistence
    {
        Task<Post> InsertPostAsync(Post post);
    }
}