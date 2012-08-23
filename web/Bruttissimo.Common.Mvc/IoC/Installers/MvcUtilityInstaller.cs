using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Common.Mvc
{
    /// <summary>
    /// Registers all internal component dependencies, such as Mvc utility classes.
    /// </summary>
    internal class MvcUtilityInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component
                    .For<ResourceCompressor>()
                    .ImplementedBy<ResourceCompressor>()
                    .LifestyleTransient()
                );

            container.Register(
                Component
                    .For<ExceptionHelper>()
                    .ImplementedBy<ExceptionHelper>()
                    .LifestyleTransient()
                );

            container.Register(
                Component
                    .For<RequestSanitizer>()
                    .ImplementedBy<RequestSanitizer>()
                    .LifestyleTransient()
                );
        }
    }
}