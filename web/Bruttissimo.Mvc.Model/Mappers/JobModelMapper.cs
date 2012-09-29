using Bruttissimo.Common.Interface;
using Bruttissimo.Domain.Entity.DTO;
using Bruttissimo.Mvc.Model.ViewModels;

namespace Bruttissimo.Mvc.Model.Mappers
{
    public class JobModelMapper : IMapperConfigurator
    {
        public void CreateMaps(IMapper mapper)
        {
            mapper.CreateMap<JobDto, JobModel>();
        }
    }
}