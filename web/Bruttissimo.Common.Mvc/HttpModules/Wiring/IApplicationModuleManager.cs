using System.Web;

namespace Bruttissimo.Common.Mvc.HttpModules.Wiring
{
    public interface IApplicationModuleManager
    {
        void Execute(HttpApplication application);
    }
}