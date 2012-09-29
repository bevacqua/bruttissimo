using System;
using Bruttissimo.Common;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Mvc.Model
{
    public class OpenGraphModelMapper : IMapperConfigurator
    {
        private readonly IUrlHelper urlHelper;

        public OpenGraphModelMapper(IUrlHelper urlHelper)
        {
            Ensure.That(urlHelper, "urlHelper").IsNotNull();

            this.urlHelper = urlHelper;
        }

        public void CreateMaps(IMapper mapper)
        {
            mapper.CreateMap<Post, OpenGraphModel>().ForMember(
                m => m.Title,
                x => x.MapFrom(p => p.Link.Title)
            ).ForMember(
                m => m.Description,
                x => x.MapFrom(p => p.Link.Description)
            ).ForMember(
                m => m.Image,
                x => x.MapFrom(p => p.Link.Picture)
            ).ForMember(
                m => m.Url,
                x => x.MapFrom(p => urlHelper.PublicRouteUrl("PostShortcut", new { id = p.Id }))
            );
        }
    }
}