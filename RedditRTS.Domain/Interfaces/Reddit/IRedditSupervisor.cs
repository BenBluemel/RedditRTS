namespace RedditRTS.Api.Domain.Interfaces.Reddit
{
    public interface IRedditSupervisor
    {
        Task CollectDataAsync();
    }
}