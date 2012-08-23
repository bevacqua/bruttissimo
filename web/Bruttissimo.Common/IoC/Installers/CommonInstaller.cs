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
        private readonly Type[] automapperProfileTypes;

        public CommonInstaller(Assembly jobAssembly, Type[] automapperProfileTypes)
        {
            if (jobAssembly == null)
            {
                throw new ArgumentNullException("jobAssembly");
            }
            if (automapperProfileTypes == null)
            {
                throw new ArgumentNullException("automapperProfileTypes");
            }
            this.jobAssembly = jobAssembly;
            this.automapperProfileTypes = automapperProfileTypes;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Install(
                new UtilityInstaller(),
                new AutoMapperInstaller(automapperProfileTypes),
                new CapabilitiesInstaller(),
                new QuartzInstaller(jobAssembly)
            );
        }
    }
}
