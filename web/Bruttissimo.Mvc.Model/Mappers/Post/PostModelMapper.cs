using System;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Interface;
using Bruttissimo.Domain.Entity.Enum;
using Bruttissimo.Domain.Service;
using Bruttissimo.Mvc.Model.ViewModels;

namespace Bruttissimo.Mvc.Model.Mappers.Post
{
    public class PostModelMapper : IMapperConfigurator
    {
        private readonly IPostService postService;

        public PostModelMapper(IPostService postService)
        {
            Ensure.That(() => postService).IsNotNull();

            this.postService = postService;
        }

        public void CreateMaps(IMapper mapper)
        {
            mapper.CreateMap<Domain.Entity.Entities.Post, PostModel>()
                .ConstructUsing(ModelByLinkType)
                .BeforeMap((src, dest) => mapper.Map(src, dest, typeof(Domain.Entity.Entities.Post), dest.GetType()))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src))
                .ForMember(
                    dest => dest.PostSlug,
                    opt => opt.MapFrom(src => postService.GetTitleSlug(src))
                )
                .ForMember(
                    dest => dest.UserMessage,
                    opt => opt.MapFrom(src => postService.BeautifyUserMessage(src.UserMessage))
                );
        }

        public PostModel ModelByLinkType(Domain.Entity.Entities.Post post)
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