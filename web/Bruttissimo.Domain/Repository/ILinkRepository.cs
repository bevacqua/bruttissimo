using System;
using Bruttissimo.Domain.Entity.Entities;

namespace Bruttissimo.Domain.Repository
{
    public interface ILinkRepository : IEntityRepository<Link>
    {
	    Link GetByReferenceUri(string referenceUri);
        Link GetByReferenceUri(Uri referenceUri);
    }
}
