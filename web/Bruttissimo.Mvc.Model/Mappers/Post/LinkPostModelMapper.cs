using Bruttissimo.Common.Helpers;
using Bruttissimo.Common.Interface;
using Bruttissimo.Mvc.Model.ViewModels;

namespace Bruttissimo.Mvc.Model.Mappers.Post
{
    public class LinkPostModelMapper : IMapperConfigurator
    {
        public void CreateMaps(IMapper mapper)
        {
            mapper.CreateMap<Domain.Entity.Entities.Post, LinkPostModel>().Ignoring(
                m => m.Comments,
                m => m.PostSlug,
                m => m.UserMessage
            );
        }
    }
}