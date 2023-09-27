using AutoMapper;

using RedditRTS.Infrastructure.Apis.Reddit.Models;

namespace RedditRTS.Infrastructure.Mapper
{
    public class RedditProfile : Profile
    {
        public RedditProfile()
        {
            CreateMap<Post, Domain.Models.Reddit.Post>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.AuthorId, src => src.MapFrom(x => x.AuthorFullname))
                .ForMember(dest => dest.AuthorName, src => src.MapFrom(x => x.Author))
                .ForMember(dest => dest.Text, src => src.MapFrom(x => x.SelfText))
                .ForMember(dest => dest.UpVotes, src => src.MapFrom(x => x.Ups))
                .ForMember(dest => dest.DownVotes, src => src.MapFrom(x => x.Downs));

            CreateMap<Root, Domain.Models.Reddit.RedditPostsResponse>()
                .ForMember(dest => dest.Posts, src => src.MapFrom(x => x.Data!.Children.Select(x => x.Data)))
                .ForMember(dest => dest.RateLimits, src => src.Ignore());
        }


    }
}
