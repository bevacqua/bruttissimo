using System.Collections.Generic;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
    public interface ICommentService
    {
        Comment Create(long postId, string message, User user, long? parentId);
        IEnumerable<Comment> GetComments(Post post);
    }
}
