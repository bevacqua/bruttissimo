using System.Collections.Generic;
using Bruttissimo.Domain.Entity.Entities;

namespace Bruttissimo.Domain.Repository
{
    public interface ICommentRepository : IEntityRepository<Comment>
    {
        IEnumerable<Comment> GetByPostId(long id);
    }
}
