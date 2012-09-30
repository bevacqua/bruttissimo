using System.Security.Principal;
using Bruttissimo.Common.Guard;
using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Domain.MiniMembership;
using Bruttissimo.Domain.Service;

namespace Bruttissimo.Domain.Logic.MiniMembership
{
    public class MiniPrincipal : IMiniPrincipal
    {
        private readonly IUserService userService;

        public IIdentity Identity { get; set; }
        public User User { get; private set; }

        public MiniPrincipal(IUserService userService, IIdentity identity)
        {
            Ensure.That(() => userService).IsNotNull();
            Ensure.That(() => identity).IsNotNull();

            this.userService = userService;
            long? id = userService.GetUserId(identity);

            if (id.HasValue)
            {
                User = userService.GetById(id.Value);
            }
            Identity = new MiniIdentity(User, identity.AuthenticationType);
        }

        public bool IsInRole(string roleOrRight)
        {
            if (User == null)
            {
                return false;
            }
            bool isInRole = userService.IsInRoleOrHasRight(User, roleOrRight);
            return isInRole;
        }
    }
}
