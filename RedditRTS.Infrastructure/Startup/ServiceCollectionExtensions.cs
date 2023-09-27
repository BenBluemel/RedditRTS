using Microsoft.Extensions.DependencyInjection;

using RedditRTS.Api.Domain.Interfaces;
using RedditRTS.Api.Domain.Interfaces.Reddit;
using RedditRTS.Api.Infrastructure.Apis.Reddit;
using RedditRTS.Api.Infrastructure.Apis.Throttling;
using RedditRTS.Api.Infrastructure.Persistance;
using RedditRTS.Domain.Interfaces.Persistence;
using RedditRTS.Infrastructure.Apis.Reddit;

namespace RedditRTS.Infrastructure.Startup
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection SetupInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IRedditService, RedditServiceFlurl>();
            services.AddSingleton<IThrottleStrategy, SmoothThrottle>();
            services.AddSingleton<IRedditSupervisor, RedditSupervisor>();
            services.AddSingleton<IRedditStatistics, RedditStatistics>();
            services.AddSingleton<IRedditPersistence, RedditPersistence>();

            //FlurlHttp.Configure(settings =>
            //{
            //    settings.AfterCallAsync = async (call) => Debug.WriteLine($"Request: {await call.Request.GetStringAsync()}");
            //    settings.BeforeCallAsync = async (call) => Debug.WriteLine($"Response: {await call.Response.GetStringAsync()}");
            //});
            return services;
        }
    }
}
