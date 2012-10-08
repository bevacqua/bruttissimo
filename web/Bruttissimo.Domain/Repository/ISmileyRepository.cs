using System.Collections.Generic;
using Bruttissimo.Domain.Entity.Entities;

namespace Bruttissimo.Domain.Repository
{
    public interface ISmileyRepository : IEntityRepository<Smiley>
    {
        IEnumerable<Smiley> GetSmileys();
    }
}