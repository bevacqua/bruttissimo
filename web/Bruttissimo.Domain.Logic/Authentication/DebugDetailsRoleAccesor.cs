using System.Web;
using Bruttissimo.Common.Mvc.Interface;
using Bruttissimo.Domain.Entity.Constants;

namespace Bruttissimo.Domain.Logic.Authentication
{
    public sealed class DebugDetailsRoleAccesor : IDebugDetailsRoleAccesor
    {
        public string[] GetAuthorizedRoles(HttpRequestBase request)
        {
            return new[] { Rights.CanAccessDebuggingDetails, Roles.Administrator };
        }
    }
}
