using System;
using System.Collections.Generic;
using Bruttissimo.Mvc.Controllers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Common.Mvc
{
	/// <summary>
	/// Registers core MVC-specific dependencies.
	/// </summary>
	public sealed class MvcInfrastructureInstaller : IWindsorInstaller
	{
		private readonly Type type;
		private readonly string defaultApplicationTitle;
		private readonly IList<ResourceAssemblyLocation> resourceAssemblyLocations;

		/// <summary>
		/// Installs all required components and dependencies for the Mvc infrastructure package.
		/// </summary>
		/// <param name="type">Any type contained in the web project assembly.</param>
		/// <param name="defaultApplicationTitle">The default title to display in ajax requests when partially rendering a view.</param>
		/// <param name="resourceAssemblyLocations">The location of the different string resources that are rendered client-side.</param>
		public MvcInfrastructureInstaller(Type type, string defaultApplicationTitle, IList<ResourceAssemblyLocation> resourceAssemblyLocations)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (defaultApplicationTitle == null)
			{
				throw new ArgumentNullException("defaultApplicationTitle");
			}
			if (resourceAssemblyLocations == null)
			{
				throw new ArgumentNullException("resourceAssemblyLocations");
			}
			this.type = type;
			this.defaultApplicationTitle = defaultApplicationTitle;
			this.resourceAssemblyLocations = resourceAssemblyLocations;
		}

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Install(
				new CommonInstaller(),
				new ComponentInstaller(),
				new AspNetInstaller(),
				new MvcViewInstaller(type),
				new MvcControllerInstaller(type, defaultApplicationTitle, resourceAssemblyLocations),
				new MvcModelValidatorInstaller(type),
				new MvcModelBinderInstaller(type),
				new SquishItInstaller()
			);
		}
	}
}