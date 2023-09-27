using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using RedditRTS.Api.Controllers.ViewModels.Posts;
using RedditRTS.Domain.Interfaces.Persistence;
using System.Net;

namespace RedditRTS.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IRedditStatistics _redditStatistics;
        private readonly IMapper _mapper;
        private readonly int maxLimit = 100;

        public PostsController(IRedditStatistics redditStatistics, IMapper mapper)
        {
            _redditStatistics = redditStatistics;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("mostupvotes")]
        public async Task<ActionResult<PostListViewModel>> PostsWithMostUpVotes(string? subreddit, int limit = 10)
        {
            if (limit > maxLimit)
            {
                return Problem(
                    title: "limit out of range",
                    detail: $"limit max is {maxLimit}",
                    statusCode: (int)HttpStatusCode.BadRequest
                );
            }

            var posts = await _redditStatistics.GetPostsWithMostUpvotesAsync(subreddit, limit);
            var result = _mapper.Map<PostListViewModel>(posts);
            return result;
        }

        [HttpGet]
        [Route("authors/mostposts")]
        public async Task<ActionResult<AuthorListViewModel>> AuthorsWithMostPosts(string? subreddit, int limit = 10)
        {
            if (limit > maxLimit)
            {
                return Problem(
                    title: "limit out of range",
                    detail: $"limit max is {maxLimit}",
                    statusCode: (int)HttpStatusCode.BadRequest
                );
            }

            var authors = await _redditStatistics.GetAuthorsWithMostPostsAsync(subreddit, limit);
            var result = _mapper.Map<AuthorListViewModel>(authors);
            return result;
        }
    }
}
