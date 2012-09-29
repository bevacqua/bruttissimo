using Bruttissimo.Common;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Mvc.Model
{
    public class LinkPostModelMapper : IMapperConfigurator
    {
        public void CreateMaps(IMapper mapper)
        {
            mapper.CreateMap<Post, LinkPostModel>().Ignoring(
                m => m.Comments,
                m => m.PostSlug
            );
        }
    }
}