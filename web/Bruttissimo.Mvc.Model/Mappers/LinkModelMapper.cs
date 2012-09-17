using Bruttissimo.Common;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Mvc.Model
{
    public class LinkModelMapper : IMapperConfigurator
    {
        public void CreateMaps(IMapper mapper)
        {
            mapper.CreateMap<Link, LinkModel>();
        }
    }
}