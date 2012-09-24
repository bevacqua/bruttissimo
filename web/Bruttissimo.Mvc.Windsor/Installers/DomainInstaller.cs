using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain.Logic;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Mvc.Windsor
{
    /// <summary>
    /// Registers all other dependencies for the Domain layer.
    /// </summary>
    public class DomainInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component
                    .For<IDebugDetailsRoleAccesor>()
                    .ImplementedBy<DebugDetailsRoleAccesor>()
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
