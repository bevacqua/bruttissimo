using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
    public interface ITwitterRepository
    {
        TwitterPost PostToFeed(Post post);
    }
}