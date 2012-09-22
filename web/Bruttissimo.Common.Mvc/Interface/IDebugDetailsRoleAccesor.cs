using System.Web;

namespace Bruttissimo.Common.Mvc
{
    public interface IDebugDetailsRoleAccesor
    {
        string[] GetAuthorizedRoles(HttpRequestBase request);
    }
}