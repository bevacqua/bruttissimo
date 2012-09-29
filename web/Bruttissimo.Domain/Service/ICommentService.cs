using System.Collections.Generic;
using Bruttissimo.Domain.Entity.Entities;

namespace Bruttissimo.Domain.Service
{
    public interface ICommentService
    {
        Comment Create(long postId, string message, User user, long? parentId);
        IEnumerable<Comment> GetComments(Post post);
    }
}
