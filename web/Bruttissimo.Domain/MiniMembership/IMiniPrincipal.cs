using System.Security.Principal;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
    public interface IMiniPrincipal : IPrincipal
    {
        User User { get; }
    }
}
