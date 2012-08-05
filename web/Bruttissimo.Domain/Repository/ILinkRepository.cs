using System;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
	public interface ILinkRepository
	{
		Link GetById(long id);
		Link GetByReferenceUri(Uri referenceUri);
		Link Insert(Link link);
	}
}
