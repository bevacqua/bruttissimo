using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Common.Mvc
{
	/// <summary>
	/// Registers core MVC-specific dependencies.
	/// </summary>
	public sealed class MvcControllerInstaller : IWindsorInstaller
	{
		private readonly Assembly assembly;
		private readonly string defaultApplicationTitle;
		private readonly IList<ResourceAssemblyLocation> resourceAssemblyLocations;

		public MvcControllerInstaller(Assembly assembly, string defaultApplicationTitle, IList<ResourceAssemblyLocation> resourceAssemblyLocations)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (defaultApplicationTitle == null)
			{
				throw new ArgumentNullException("defaultApplicationTitle");
			}
			if (resourceAssemblyLocations == null)
			{
				throw new ArgumentNullException("resourceAssemblyLocations");
			}
			this.assembly = assembly;
			this.defaultApplicationTitle = defaultApplicationTitle;
			this.resourceAssemblyLocations = resourceAssemblyLocations;
		}

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			// Registers all controllers from the controller assembly.
			container.Register(
				Classes
					.FromAssembly(assembly)
					.BasedOn<IController>()
					.LifestylePerWebRequest()
			);

			// Registers all controllers from this assembly.
			container.Register(
				Classes
					.FromThisAssembly()
					.BasedOn<IController>()
					.LifestylePerWebRequest()
			);

			// Register our action invoker injector.
			container.Register(
				Component
					.For<IActionInvoker>()
					.UsingFactoryMethod(InstanceActionInvoker)
					.LifestylePerWebRequest()
			);

			// Register the assembly and namespaces different resource strings are located in. Used by the ResourceController.
			container.Register(
				Component
					.For<IList<ResourceAssemblyLocation>>()
					.UsingFactoryMethod(() => resourceAssemblyLocations)
					.LifestyleTransient()
			);
		}

		private IActionInvoker InstanceActionInvoker(IKernel kernel, ComponentModel model, CreationContext context)
		{
			Type loggerType = context.Handler.ComponentModel.Implementation;
			ExceptionHelper exceptionHelper = kernel.Resolve<ExceptionHelper>();
			ErrorHandlingAttribute errorHandling = new ErrorHandlingAttribute(loggerType, exceptionHelper);

			AjaxTransformAttribute ajaxTransform = new AjaxTransformAttribute(defaultApplicationTitle);

			IList<IActionFilter> actionFilters = new List<IActionFilter> { ajaxTransform };
			IList<IAuthorizationFilter> authorizationFilters = new List<IAuthorizationFilter> { };
			IList<IExceptionFilter> exceptionFilters = new List<IExceptionFilter> { errorHandling };
			IList<IResultFilter> resultFilters = new List<IResultFilter> { };

			return new WindsorActionInvoker(actionFilters, authorizationFilters, exceptionFilters, resultFilters);
		}
	}
}