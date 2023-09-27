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

        /// <summary>
        /// Gets a list of authors with the most posts
        /// </summary>
        /// <param name="subreddit">The subreddit name only ie "Home" instead of "r/Home". Optional, will return all subreddits if left empty.</param>
        /// <param name="limit">The number of authors to show, default 10, max 100</param>
        /// <returns>A list of authors with the most posts in a subreddit</returns>
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

        /// <summary>
        /// Gets a list of the most upvoted posts, ordered by upvotes descending
        /// </summary>
        /// <param name="subreddit">The subreddit name only ie "Home" instead of "r/Home". Optional, will return all subreddits if left empty.</param>
        /// <param name="limit">The number of posts to show, default 10, max 100</param>
        /// <returns>A list of the most upvoted posts</returns>
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
