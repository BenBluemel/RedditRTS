using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using RedditRTS.Api.Domain.Interfaces.Reddit;

namespace RedditRTS.Api.Workers.SubredditWorker
{
    public class SubredditWorker : BackgroundService
    {
        private readonly IRedditSupervisor _redditSupervisor;
        private readonly ILogger<SubredditWorker> _logger;

        public SubredditWorker(IRedditSupervisor redditSupervisor, ILogger<SubredditWorker> logger)
        {
            _redditSupervisor = redditSupervisor;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                try
                {
                    await _redditSupervisor.CollectDataAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unhandled exception");
                }
            }
        }

        
    }
}
