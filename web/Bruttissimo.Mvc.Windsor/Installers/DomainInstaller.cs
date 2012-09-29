using Bruttissimo.Common.Mvc;
using Bruttissimo.Common.Mvc.Interface;
using Bruttissimo.Domain.Logic.Authentication;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Mvc.Windsor.Installers
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
        }
    }
}
