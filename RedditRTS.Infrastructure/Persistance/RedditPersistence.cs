using Microsoft.Extensions.Logging;

using RedditRTS.Api.Domain.Interfaces.Reddit;
using RedditRTS.Domain.Models.Reddit;

namespace RedditRTS.Api.Infrastructure.Persistance
{
    public class RedditPersistence : IRedditPersistence
    {
        private readonly ILogger<RedditPersistence> _logger;

        public RedditPersistence(ILogger<RedditPersistence> logger)
        {
            _logger = logger;
        }
        public Task<Post> InsertPostAsync(Post post)
        {
            if (post?.Id == null)
            {
                throw new ArgumentNullException(nameof(post));
            }

            if (!RedditMemoryStorage.Posts.TryAdd(post.Id, post))
            {
                _logger.LogWarning("Duplicate Key: {Id}", post.Id);
            }
            return Task.FromResult(post);
        }


    }
}
