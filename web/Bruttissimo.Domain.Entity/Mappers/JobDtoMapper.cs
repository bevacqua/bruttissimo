using System;
using Bruttissimo.Common.Extensions;
using Bruttissimo.Common.Interface;
using Bruttissimo.Domain.Entity.DTO;

namespace Bruttissimo.Domain.Entity.Mappers
{
    public class JobDtoMapper : IMapperConfigurator
    {
        public void CreateMaps(IMapper mapper)
        {
            mapper.CreateMap<Type, JobDto>().ForMember(
                m => m.Name,
                x => x.MapFrom(t => t.Name.Replace("Job", string.Empty).SplitOnCamelCase())
            ).ForMember(
                m => m.Guid,
                x => x.MapFrom(t => t.GUID.Stringify())
            );
        }
    }
}