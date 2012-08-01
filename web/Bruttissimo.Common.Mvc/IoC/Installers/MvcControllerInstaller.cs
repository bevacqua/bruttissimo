using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bruttissimo.Mvc.Controllers;
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
		private readonly Type type;
		private readonly string defaultApplicationTitle;
		private readonly IList<ResourceAssemblyLocation> resourceAssemblyLocations;

		public MvcControllerInstaller(Type type, string defaultApplicationTitle, IList<ResourceAssemblyLocation> resourceAssemblyLocations)
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
			// Registers all controllers from the installing assembly.
			container.Register(
				Classes
					.FromAssemblyContaining(type)
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

			AjaxTransformActionResultAttribute ajaxTransformActionResult = new AjaxTransformActionResultAttribute(defaultApplicationTitle);

			IList<IActionFilter> actionFilters = new List<IActionFilter> { ajaxTransformActionResult };
			IList<IAuthorizationFilter> authorizationFilters = new List<IAuthorizationFilter> { };
			IList<IExceptionFilter> exceptionFilters = new List<IExceptionFilter> { errorHandling };
			IList<IResultFilter> resultFilters = new List<IResultFilter> { };

			return new WindsorActionInvoker(actionFilters, authorizationFilters, exceptionFilters, resultFilters);
		}
	}
}