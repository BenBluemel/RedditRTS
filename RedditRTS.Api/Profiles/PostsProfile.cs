using AutoMapper;

using RedditRTS.Api.Controllers.ViewModels.Posts;
using RedditRTS.Domain.Models.Reddit;

namespace RedditRTS.Api.Profiles
{
    public class PostsProfile : Profile
    {
        public PostsProfile()
        {
            CreateMap<Post, PostViewModel>();
            CreateMap<IEnumerable<Post>, PostListViewModel>()
                .ForMember(dest => dest.Posts, src => src.MapFrom(x => x));
        }
    }
}
