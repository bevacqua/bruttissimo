using System.Collections.Generic;
using Bruttissimo.Common;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Mvc.Model
{
    public class PostListModelMapper : IMapperConfigurator
    {
        public void CreateMaps(IMapper mapper)
        {
            mapper.CreateMap<IEnumerable<Post>, PostListModel>().ConvertUsing<PostListFromEntitiesConverter>();
        }
    }
}