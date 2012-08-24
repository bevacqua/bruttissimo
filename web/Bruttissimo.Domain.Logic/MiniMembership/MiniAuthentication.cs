using System;
using System.Web;

namespace Bruttissimo.Domain.Logic
{
    public class MiniAuthentication
    {
        private readonly IUserService userService;
        private readonly HttpContextBase context;

        public MiniAuthentication(IUserService userService, HttpContextBase context)
        {
            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
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
