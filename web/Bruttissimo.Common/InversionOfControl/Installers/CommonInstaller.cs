using System.Reflection;
using Bruttissimo.Common.Guard;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Common.InversionOfControl.Installers
{
    /// <summary>
    /// Registers all common dependencies.
    /// </summary>
    public sealed class CommonInstaller : IWindsorInstaller
    {
        private readonly Assembly jobAssembly;
        private readonly Assembly[] mapperAssemblies;

        public CommonInstaller(Assembly jobAssembly, Assembly[] mapperAssemblies)
        {
            Ensure.That(() => jobAssembly).IsNotNull();
            Ensure.That(() => mapperAssemblies).IsNotNull();
            this.jobAssembly = jobAssembly;
            this.mapperAssemblies = mapperAssemblies;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Install(
                new UtilityInstaller(),
                new AutoMapperInstaller(mapperAssemblies),
                new CapabilitiesInstaller(),
                new QuartzInstaller(jobAssembly)
            );
        }
    }
}
