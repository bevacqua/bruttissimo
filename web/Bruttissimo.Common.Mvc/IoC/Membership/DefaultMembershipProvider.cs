using System.Web.Security;

namespace Bruttissimo.Common.Mvc
{
    public class DefaultMembershipProvider : IMembershipProvider
    {
        public bool ValidateUser(string username, string password)
        {
            return Membership.ValidateUser(username, password);
        }
    }
}
