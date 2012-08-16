using System;
using System.Security.Principal;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
	public class MiniPrincipal : IMiniPrincipal
	{
		private readonly IUserService userService;

		public IIdentity Identity { get; set; }
		public User User { get; private set; }

		public MiniPrincipal(IUserService userService, IIdentity identity)
		{
			if (userService == null)
			{
				throw new ArgumentNullException("userService");
			}
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
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