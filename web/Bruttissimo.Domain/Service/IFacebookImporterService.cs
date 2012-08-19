using System.Collections.Generic;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
    public interface IFacebookImporterService
    {
        void Import(IEnumerable<FacebookPost> posts);
    }
}
