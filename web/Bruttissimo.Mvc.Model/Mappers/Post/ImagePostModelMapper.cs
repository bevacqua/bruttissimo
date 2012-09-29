using Bruttissimo.Common;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Mvc.Model
{
    public class ImagePostModelMapper : IMapperConfigurator
    {
        public void CreateMaps(IMapper mapper)
        {
            mapper.CreateMap<Post, ImagePostModel>().Ignoring(
                m => m.Comments,
                m => m.PostSlug
            );
        }
    }
}