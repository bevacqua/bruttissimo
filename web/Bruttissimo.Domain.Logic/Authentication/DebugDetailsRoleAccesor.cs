using System.Web;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
    public sealed class DebugDetailsRoleAccesor : IDebugDetailsRoleAccesor
    {
        public string[] GetAuthorizedRoles(HttpRequestBase request)
        {
            return new[] { Rights.CanAccessDebuggingDetails, Roles.Administrator };
        }
    }
}
