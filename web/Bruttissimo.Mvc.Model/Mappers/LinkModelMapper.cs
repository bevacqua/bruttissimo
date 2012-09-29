using Bruttissimo.Common.Interface;
using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Mvc.Model.ViewModels;

namespace Bruttissimo.Mvc.Model.Mappers
{
    public class LinkModelMapper : IMapperConfigurator
    {
        public void CreateMaps(IMapper mapper)
        {
            mapper.CreateMap<Link, LinkModel>();
        }
    }
}