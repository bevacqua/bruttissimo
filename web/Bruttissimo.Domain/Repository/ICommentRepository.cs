using System.Collections.Generic;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
    public interface ICommentRepository : IEntityRepository<Comment>
    {
        IEnumerable<Comment> GetByPostId(long id);
    }
}
