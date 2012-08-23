using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Common.Mvc
{
    /// <summary>
    /// Registers core MVC-specific dependencies.
    /// </summary>
    public sealed class MvcInstaller : IWindsorInstaller
    {
        private readonly MvcInstallerParameters parameters;

        /// <summary>
        /// Installs all required components and dependencies for the Mvc infrastructure package.
        /// </summary>
        public MvcInstaller(MvcInstallerParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }
            this.parameters = parameters;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Install(
                new AspNetInstaller(),
                new MvcViewInstaller(parameters.ViewAssembly),
                new MvcControllerInstaller(parameters),
                new MvcModelValidatorInstaller(parameters.ModelAssembly),
                new MvcModelBinderInstaller(parameters.ModelAssembly),
                new MvcUtilityInstaller(),
                new SquishItInstaller()
            );
        }
    }
}
