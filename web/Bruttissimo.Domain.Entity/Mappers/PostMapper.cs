using Bruttissimo.Common;
using Bruttissimo.Common.Helpers;
using Bruttissimo.Common.Interface;
using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Domain.Entity.Social.Facebook;

namespace Bruttissimo.Domain.Entity.Mappers
{
    public class PostMapper : IMapperConfigurator
    {
        public void CreateMaps(IMapper mapper)
        {
            mapper.CreateMap<FacebookPost, Post>().ForMember(
                m => m.FacebookPostId,
                x => x.MapFrom(p => p.Id)
            ).ForMember(
                m => m.FacebookFeedId,
                x => x.MapFrom(p => p.To.Data[0].Id)
            ).ForMember(
                m => m.FacebookUserId,
                x => x.MapFrom(p => p.From.Id)
            ).ForMember(
                m => m.UserMessage,
                x => x.MapFrom(p => p.Message)
            ).ForMember(
                m => m.Created,
                x => x.MapFrom(p => p.CreatedTime)
            ).Ignoring(
                m => m.Id,
                m => m.UserId,
                m => m.User,
                m => m.LinkId,
                m => m.Link,
                m => m.Updated,
                m => m.TwitterPostId,
                m => m.TwitterUserId,
                m => m.Comments
            );
        }
    }
}