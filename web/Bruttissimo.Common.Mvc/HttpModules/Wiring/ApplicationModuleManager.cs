using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bruttissimo.Common.Helpers;
using Bruttissimo.Common.InversionOfControl;

namespace Bruttissimo.Common.Mvc.HttpModules.Wiring
{
    public sealed class ApplicationModuleManager : IApplicationModuleManager
    {
        public void Execute(HttpApplication application)
        {
            IHttpModule[] modules = IoC.Container.ResolveAll<IHttpModule>();
            IEnumerable<IHttpModule> filtered = modules.Where(m => m.GetType().HasAttribute<ApplicationModuleAttribute>());
            IEnumerable<IHttpModule> ordered = filtered.OrderByDescending(m => m.GetType().GetAttribute<ApplicationModuleAttribute>().Priority);

            foreach (IHttpModule module in ordered)
            {
                module.Init(application);
            }
        }
    }
}