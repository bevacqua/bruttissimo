using Bruttissimo.Common;
using Bruttissimo.Domain.Entity;
using TweetSharp;

namespace Bruttissimo.Domain.Social
{
    public class TwitterPostMapper : IMapperConfigurator
    {
        public void CreateMaps(IMapper mapper)
        {
            mapper.CreateMap<TwitterStatus, TwitterPost>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FromId, opt => opt.MapFrom(src => src.User.Id));
        }
    }
}