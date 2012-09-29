using System;
using System.Web;
using Bruttissimo.Common.Guard;

namespace Bruttissimo.Domain.Logic
{
    public class MiniAuthentication
    {
        private readonly IUserService userService;
        private readonly HttpContextBase context;

        public MiniAuthentication(IUserService userService, HttpContextBase context)
        {
            Ensure.That(userService, "userService").IsNotNull();
            Ensure.That(context, "context").IsNotNull();

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
