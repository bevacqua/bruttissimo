using System.Web;
using Bruttissimo.Common.Mvc.HttpModules.Wiring;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Common.Mvc.InversionOfControl.Installers
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

            container.Register(
                Component
                    .For<IApplicationModuleManager>()
                    .ImplementedBy<ApplicationModuleManager>()
                    .LifestyleTransient()
                );
        }
    }
}
