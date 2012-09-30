using System.Reflection;
using Bruttissimo.Common.Guard;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentValidation;

namespace Bruttissimo.Common.Mvc.InversionOfControl.Installers
{
    /// <summary>
    /// Registers all fluent validators.
    /// </summary>
    internal sealed class MvcModelValidatorInstaller : IWindsorInstaller
    {
        private readonly Assembly assembly;

        public MvcModelValidatorInstaller(Assembly assembly)
        {
            Ensure.That(() => assembly).IsNotNull();

            this.assembly = assembly;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Register validators in this assembly, such as NullModelValidator.
            container.Register(
                AllTypes
                    .FromThisAssembly()
                    .BasedOn(typeof(IValidator<>))
                    .WithServiceBase()
                    .LifestyleTransient()
                );

            // Register validators in model project assembly.
            container.Register(
                AllTypes
                    .FromAssembly(assembly)
                    .BasedOn(typeof(IValidator<>))
                    .WithServiceBase()
                    .LifestyleTransient()
                );
        }
    }
}
