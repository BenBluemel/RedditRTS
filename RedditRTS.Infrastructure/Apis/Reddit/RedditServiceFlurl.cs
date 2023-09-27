using AutoMapper;
using Flurl;
using Flurl.Http;
using Flurl.Util;
using Microsoft.Extensions.Options;
using RedditRTS.Api.Domain.Interfaces.Reddit;
using RedditRTS.Domain.Models.Configuration;
using RedditRTS.Domain.Models.Reddit;
using RedditRTS.Domain.Models.Throttling;
using RedditRTS.Infrastructure.Apis.Reddit.Models;
using System.Diagnostics;

namespace RedditRTS.Infrastructure.Apis.Reddit
{
    public class RedditServiceFlurl : IRedditService
    {
        private readonly IMapper _mapper;
        private readonly RedditConfig _redditConfig;

        public RedditServiceFlurl(IOptions<RedditConfig> reditConfig, IMapper mapper)
        {
            _redditConfig = reditConfig.Value ?? throw new ArgumentNullException(nameof(reditConfig));
            _mapper = mapper;
        }
        
        public async Task<RedditPostsResponse> GetNextPostsAsync(string subreddit, string? after)
        {
            FlurlHttp.Configure(settings =>
            {
                settings.AfterCall = (call) => Debug.WriteLine(call.Request);
                settings.BeforeCall = (call) => Debug.WriteLine(call.Request);
            });

            var token = _redditConfig.ApiKey;

            var flurlResponse = await new Url(_redditConfig.ApiHost)
                .AppendPathSegment(subreddit)
                .AppendPathSegment("new")
                .WithHeader("User-Agent", "Hello")
                .SetQueryParam("after", after)
                .SetQueryParam("limit", 100)
                .WithOAuthBearerToken(token)
                .GetAsync();
            var rawResponse = await flurlResponse.GetJsonAsync<Root>();

            var result = _mapper.Map<RedditPostsResponse>(rawResponse);
            result.RateLimits = GetRateLimits(flurlResponse.Headers);
            
            return result;
        }

        public RateLimits? GetRateLimits(IReadOnlyNameValueList<string> headers)
        {
            if (headers.Contains("x-ratelimit-remaining") && headers.Contains("x-ratelimit-used") && headers.Contains("x-ratelimit-reset"))
            {
                return new RateLimits
                {
                    RateLimitRemaining = GetDoubleHeader("x-ratelimit-remaining", headers) ?? 0d,
                    RateLimitUsed = GetIntHeader("x-ratelimit-used", headers) ?? 0,
                    RateLimitReset = GetIntHeader("x-ratelimit-reset", headers) ?? 0
                };
            }

            return null;
        }

        public int? GetIntHeader(string name, IReadOnlyNameValueList<string> headers)
        {
            if (headers.TryGetFirst(name, out string value) && int.TryParse(value, out var result))
            {
                return result;
            }
            return null;
        }

        public double? GetDoubleHeader(string name, IReadOnlyNameValueList<string> headers)
        {
            if (headers.TryGetFirst(name, out string value) && double.TryParse(value, out var result))
            {
                return result;
            }
            return null;
        }
    }
}
