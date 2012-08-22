using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
    public interface ICommentService
    {
        Comment Create(long postId, string comment);
    }
}
