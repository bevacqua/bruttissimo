using System.Web;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Common.Mvc
{
    /// <summary>
    /// Registers all HttpModules required by our web application.
    /// </summary>
    internal sealed class HttpModuleInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes
                    .FromThisAssembly()
                    .BasedOn<IHttpModule>()
                    .WithServiceFromInterface()
                    .LifestyleTransient()
                );
        }
    }
}
