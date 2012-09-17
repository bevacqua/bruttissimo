using System;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Common
{
    /// <summary>
    /// Registers all common dependencies.
    /// </summary>
    public sealed class CommonInstaller : IWindsorInstaller
    {
        private readonly Assembly jobAssembly;
        private readonly Assembly[] automapperAssemblies;

        public CommonInstaller(Assembly jobAssembly, Assembly[] automapperAssemblies)
        {
            if (jobAssembly == null)
            {
                throw new ArgumentNullException("jobAssembly");
            }
            if (automapperAssemblies == null)
            {
                throw new ArgumentNullException("automapperAssemblies");
            }
            this.jobAssembly = jobAssembly;
            this.automapperAssemblies = automapperAssemblies;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Install(
                new UtilityInstaller(),
                new AutoMapperInstaller(automapperAssemblies),
                new CapabilitiesInstaller(),
                new QuartzInstaller(jobAssembly)
            );
        }
    }
}
