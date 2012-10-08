using Bruttissimo.Common.Helpers;
using Bruttissimo.Common.Interface;
using Bruttissimo.Mvc.Model.ViewModels;

namespace Bruttissimo.Mvc.Model.Mappers.Post
{
    public class ImagePostModelMapper : IMapperConfigurator
    {
        public void CreateMaps(IMapper mapper)
        {
            mapper.CreateMap<Domain.Entity.Entities.Post, ImagePostModel>().Ignoring(
                m => m.Comments,
                m => m.PostSlug,
                m => m.UserMessage
            );
        }
    }
}