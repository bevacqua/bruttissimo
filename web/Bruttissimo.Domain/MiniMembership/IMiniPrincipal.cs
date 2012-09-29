using System.Security.Principal;
using Bruttissimo.Domain.Entity.Entities;

namespace Bruttissimo.Domain.MiniMembership
{
    public interface IMiniPrincipal : IPrincipal
    {
        User User { get; }
    }
}
