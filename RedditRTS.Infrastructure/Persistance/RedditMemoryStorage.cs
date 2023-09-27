using RedditRTS.Domain.Models.Reddit;
using System.Collections.Concurrent;

namespace RedditRTS.Api.Infrastructure.Persistance
{
    internal static class RedditMemoryStorage
    {
        internal static ConcurrentDictionary<string, Post> Posts = new();
    }
}
