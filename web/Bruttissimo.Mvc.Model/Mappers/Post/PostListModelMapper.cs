using System.Collections.Generic;
using System.Linq;
using Bruttissimo.Common;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Mvc.Model
{
    public class PostListModelMapper : IMapperConfigurator
    {
        public void CreateMaps(IMapper mapper)
        {
            int pageSize = Config.Defaults.PostListPageSize;

            mapper.CreateMap<IEnumerable<Post>, PostListModel>()
                .ForMember(dest => dest.Posts, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.OpenGraph, opt => opt.MapFrom(src => src.FirstOrDefault()))
                .ForMember(dest => dest.HasMorePosts, opt => opt.MapFrom(src => pageSize >= src.Count()));
        }
    }
}