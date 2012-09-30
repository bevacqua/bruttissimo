using System.Web;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Interface;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Common.Mvc.Extensions;
using Bruttissimo.Domain.Entity.DTO;
using Bruttissimo.Domain.Service;
using Bruttissimo.Mvc.Model.ViewModels;

namespace Bruttissimo.Mvc.Model.Mappers
{
    public class ScheduledJobModelMapper : IMapperConfigurator
    {
		private readonly IUserService userService;

        public ScheduledJobModelMapper(IUserService userService)
        {
            Ensure.That(() => userService).IsNotNull();

            this.userService = userService;
        }

        public void CreateMaps(IMapper mapper)
        {
            mapper.CreateMap<ScheduledJobDto, ScheduledJobModel>().ForMember(
                m => m.StartTime,
                x => x.MapFrom(j => userService.ToCurrentUserTimeZone(HttpContext.Current.Wrap(), j.StartTime))
            );
        }
    }
}