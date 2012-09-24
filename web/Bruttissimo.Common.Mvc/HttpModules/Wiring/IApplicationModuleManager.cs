using System.Web;

namespace Bruttissimo.Common.Mvc
{
    public interface IApplicationModuleManager
    {
        void Execute(HttpApplication application);
    }
}