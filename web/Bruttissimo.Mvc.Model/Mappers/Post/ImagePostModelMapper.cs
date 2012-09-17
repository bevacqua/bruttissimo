using System;
using Bruttissimo.Common;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Mvc.Model
{
    public class ImagePostModelMapper : IMapperConfigurator
    {
        private readonly IPostService postService;

        public ImagePostModelMapper(IPostService postService)
        {
            if (postService == null)
            {
                throw new ArgumentNullException("postService");
            }
            this.postService = postService;
        }

        public void CreateMaps(IMapper mapper)
        {
            mapper.CreateMap<Post, ImagePostModel>().Ignoring(
                m => m.Comments,
                m => m.PostSlug
            );
        }
    }
}