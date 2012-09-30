using System.Web;
using Bruttissimo.Common.Guard;
using Bruttissimo.Domain.Service;

namespace Bruttissimo.Domain.Logic.MiniMembership
{
    public class MiniAuthentication
    {
        private readonly IUserService userService;
        private readonly HttpContextBase context;

        public MiniAuthentication(IUserService userService, HttpContextBase context)
        {
            Ensure.That(() => userService).IsNotNull();
            Ensure.That(() => context).IsNotNull();

            this.userService = userService;
            this.context = context;
        }

        public void SetContextPrincipal()
        {
            if (context.Request.IsAuthenticated)
            {
                context.User = new MiniPrincipal(userService, context.User.Identity);
            }
        }
    }
}
