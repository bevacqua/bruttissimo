using System;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
    public interface ILinkRepository : IEntityRepository<Link>
    {
	    Link GetByReferenceUri(string referenceUri);
        Link GetByReferenceUri(Uri referenceUri);
    }
}
