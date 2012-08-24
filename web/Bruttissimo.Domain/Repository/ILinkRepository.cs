using System;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
    public interface ILinkRepository : IEntityRepository<Link>
    {
        Link GetByReferenceUri(Uri referenceUri);
    }
}
