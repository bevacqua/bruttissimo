using Bruttissimo.Common;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Mvc.Model
{
    public class CommentListModelMapper : IMapperConfigurator
    {
        public void CreateMaps(IMapper mapper)
        {
            mapper.CreateMap<Post, CommentListModel>().ConvertUsing<CommentListFromEntitiesConverter>();
        }
    }
}