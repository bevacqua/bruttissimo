using System;
using Bruttissimo.Common;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Mvc.Model
{
    public class PostModelMapper : IMapperConfigurator
    {
        private readonly IPostService postService;

        public PostModelMapper(IPostService postService)
        {
            if (postService == null)
            {
                throw new ArgumentNullException("postService");
            }
            this.postService = postService;
        }

        public void CreateMaps(IMapper mapper)
        {
            mapper.CreateMap<Post, PostModel>()
                .ConstructUsing(ModelByLinkType)
                .BeforeMap((src, dest) => mapper.Map(src, dest, typeof(Post), dest.GetType()))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src))
                .ForMember(
                    dest => dest.PostSlug,
                    opt => opt.MapFrom(src => postService.GetTitleSlug(src))
                )
                .Ignoring(dest => dest.Comments);
        }

        public PostModel ModelByLinkType(Post post)
        {
            switch (post.Link.Type)
            {
                default:
                {
                    throw new ArgumentOutOfRangeException("post.Link.Type");
                }
                case LinkType.Html:
                {
                    return new LinkPostModel();
                }
                case LinkType.Image:
                {
                    return new ImagePostModel();
                }
            }
        }
    }
}