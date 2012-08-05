using System;
using System.Collections.Generic;
using System.Reflection;
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
		private readonly Assembly modelAssembly;
		private readonly Assembly viewAssembly;
		private readonly Assembly controllerAssembly;
		private readonly string defaultApplicationTitle;
		private readonly IList<ResourceAssemblyLocation> resourceAssemblyLocations;

		/// <summary>
		/// Installs all required components and dependencies for the Mvc infrastructure package.
		/// </summary>
		/// <param name="modelAssembly">The model assembly.</param>
		/// <param name="viewAssembly">The view assembly.</param>
		/// <param name="controllerAssembly">The controller assembly.</param>
		/// <param name="defaultApplicationTitle">The default title to display in ajax requests when partially rendering a view.</param>
		/// <param name="resourceAssemblyLocations">The location of the different string resources that are rendered client-side.</param>
		public MvcInfrastructureInstaller(Assembly modelAssembly, Assembly viewAssembly, Assembly controllerAssembly, string defaultApplicationTitle, IList<ResourceAssemblyLocation> resourceAssemblyLocations)
		{
			if (modelAssembly == null)
			{
				throw new ArgumentNullException("modelAssembly");
			}
			if (viewAssembly == null)
			{
				throw new ArgumentNullException("viewAssembly");
			}
			if (controllerAssembly == null)
			{
				throw new ArgumentNullException("controllerAssembly");
			}
			if (defaultApplicationTitle == null)
			{
				throw new ArgumentNullException("defaultApplicationTitle");
			}
			if (resourceAssemblyLocations == null)
			{
				throw new ArgumentNullException("resourceAssemblyLocations");
			}
			this.modelAssembly = modelAssembly;
			this.viewAssembly = viewAssembly;
			this.controllerAssembly = controllerAssembly;
			this.defaultApplicationTitle = defaultApplicationTitle;
			this.resourceAssemblyLocations = resourceAssemblyLocations;
		}

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Install(
				new CommonInstaller(),
				new ComponentInstaller(),
				new AspNetInstaller(),
				new MvcViewInstaller(viewAssembly),
				new MvcControllerInstaller(controllerAssembly, defaultApplicationTitle, resourceAssemblyLocations),
				new MvcModelValidatorInstaller(modelAssembly),
				new MvcModelBinderInstaller(modelAssembly),
				new SquishItInstaller()
			);
		}
	}
}