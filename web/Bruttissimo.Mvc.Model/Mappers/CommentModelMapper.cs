using Bruttissimo.Common;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Mvc.Model
{
    public class CommentModelMapper : IMapperConfigurator
    {
        public void CreateMaps(IMapper mapper)
        {
            mapper.CreateMap<Comment, CommentModel>();
        }
    }
}