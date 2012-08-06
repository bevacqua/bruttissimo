using System;
using System.Collections.Generic;
using System.Reflection;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Mvc.Controller;
using Bruttissimo.Mvc.Model;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Mvc
{
	/// <summary>
	/// Installs all dependencies into the container. Acts as the composition root.
	/// </summary>
	public class WindsorInstaller : IWindsorInstaller
	{
		private readonly Assembly viewAssembly;
		private readonly string applicationTitle;
		private readonly IList<ResourceAssemblyLocation> resourceAssemblyLocations;

		/// <summary>
		/// Installs all required components and dependencies for the application.
		/// </summary>
		/// <param name="viewAssembly">The view assembly.</param>
		/// <param name="applicationTitle">The default title to display in ajax requests when partially rendering a view.</param>
		/// <param name="resourceAssemblyLocations">The location of the different string resources that are rendered client-side.</param>
		public WindsorInstaller(Assembly viewAssembly, string applicationTitle, IList<ResourceAssemblyLocation> resourceAssemblyLocations)
		{
			if (viewAssembly == null)
			{
				throw new ArgumentNullException("viewAssembly");
			}
			if (applicationTitle == null)
			{
				throw new ArgumentNullException("applicationTitle");
			}
			if (resourceAssemblyLocations == null)
			{
				throw new ArgumentNullException("resourceAssemblyLocations");
			}
			this.viewAssembly = viewAssembly;
			this.applicationTitle = applicationTitle;
			this.resourceAssemblyLocations = resourceAssemblyLocations;
		}

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			Assembly modelAssembly = typeof(UserLoginModel).Assembly;
			Assembly controllerAssembly = typeof(HomeController).Assembly;

			container.Install(
				new MvcInfrastructureInstaller(modelAssembly, viewAssembly, controllerAssembly, applicationTitle, resourceAssemblyLocations),
				new MiniMembershipInstaller(),
				new ServiceInstaller(),
				new RepositoryInstaller(),
				new LibraryInstaller(),
				new AutoMapperProfileInstaller() // this installer needs to resolve dependencies such as repositories through the container itself, so it goes last.
			);
		}
	}
}