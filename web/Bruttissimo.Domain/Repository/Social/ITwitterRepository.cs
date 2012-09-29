using Bruttissimo.Domain.DTO.Twitter;
using Bruttissimo.Domain.Entity.Social.Twitter;

namespace Bruttissimo.Domain.Repository.Social
{
    public interface ITwitterRepository
    {
        TwitterPost PostToFeed(string message, TwitterServiceParams serviceParams = null);
    }
}