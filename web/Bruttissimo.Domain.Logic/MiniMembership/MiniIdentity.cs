using System.Security.Principal;
using Bruttissimo.Domain.Entity.Entities;

namespace Bruttissimo.Domain.Logic.MiniMembership
{
    public class MiniIdentity : IIdentity
    {
        private readonly User user;

        public User User
        {
            get { return user; }
        }

        public MiniIdentity(User user, string authenticationType)
        {
            this.user = user;
            AuthenticationType = authenticationType;
        }

        public string AuthenticationType { get; private set; }

        public string Name
        {
            get { return user == null ? Common.Resources.User.UnregisteredUserDisplayName : user.DisplayName; }
        }

        public bool IsAuthenticated
        {
            get { return user != null; }
        }
    }
}
