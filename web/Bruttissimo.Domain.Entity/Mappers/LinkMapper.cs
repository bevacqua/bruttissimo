using Bruttissimo.Common;

namespace Bruttissimo.Domain.Entity
{
    public class LinkMapper : IMapperConfigurator
    {
        public void CreateMaps(IMapper mapper)
        {
            mapper.CreateMap<FacebookPost, Link>().ForMember(
                m => m.ReferenceUri,
                x => x.MapFrom(p => p.Link)
            ).ForMember(
                m => m.Title,
                x => x.MapFrom(p => p.Name)
            ).ForMember(
                m => m.Description,
                x => x.MapFrom(p => p.Description)
            ).ForMember(
                m => m.Picture,
                x => x.MapFrom(p => p.Picture)
            ).ForMember(
                m => m.Type,
                x => x.MapFrom(p => LinkType.Html)
            ).ForMember(
                m => m.Created,
                x => x.MapFrom(p => p.CreatedTime)
            ).Ignoring(
                m => m.Id,
                m => m.PostId
            );

        }
    }
}