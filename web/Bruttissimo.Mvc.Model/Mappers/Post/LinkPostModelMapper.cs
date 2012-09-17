using System;
using Bruttissimo.Common;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Mvc.Model
{
    public class LinkPostModelMapper : IMapperConfigurator
    {
        private readonly IPostService postService;

        public LinkPostModelMapper(IPostService postService)
        {
            if (postService == null)
            {
                throw new ArgumentNullException("postService");
            }
            this.postService = postService;
        }

        public void CreateMaps(IMapper mapper)
        {
            mapper.CreateMap<Post, LinkPostModel>().ForMember(
                m => m.PostSlug,
                x => x.MapFrom(p => postService.GetTitleSlug(p))
            ).Ignoring(m => m.Comments);
        }
    }
}