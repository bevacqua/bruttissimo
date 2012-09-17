using Bruttissimo.Common;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Mvc.Model
{
    public class LogModelMapper : IMapperConfigurator
    {
        public void CreateMaps(IMapper mapper)
        {
            mapper.CreateMap<Log, LogModel>();
        }
    }
}