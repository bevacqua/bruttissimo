using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentValidation;

namespace Bruttissimo.Common.Mvc
{
	/// <summary>
	/// Registers all fluent validators.
	/// </summary>
	public sealed class MvcModelValidatorInstaller : IWindsorInstaller
	{
		private readonly Type type;

		public MvcModelValidatorInstaller(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.type = type;
		}

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			// Register validators in this assembly, such as NullModelValidator.
			container.Register(
				AllTypes
					.FromThisAssembly()
					.BasedOn(typeof(IValidator<>))
					.WithServiceBase()
					.LifestylePerWebRequest()
			);

			// Register validators in web project assembly.
			container.Register(
				AllTypes
					.FromAssemblyContaining(type)
					.BasedOn(typeof(IValidator<>))
					.WithServiceBase()
					.LifestylePerWebRequest()
			);
		}
	}
}