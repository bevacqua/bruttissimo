using System.Web;

namespace Bruttissimo.Common.Mvc.Interface
{
    public interface IDebugDetailsRoleAccesor
    {
        string[] GetAuthorizedRoles(HttpRequestBase request);
    }
}