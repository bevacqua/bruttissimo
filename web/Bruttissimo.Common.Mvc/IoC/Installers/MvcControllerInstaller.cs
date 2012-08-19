using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using log4net;

namespace Bruttissimo.Common.Mvc
{
    /// <summary>
    /// Registers core MVC-specific dependencies.
    /// </summary>
    public sealed class MvcControllerInstaller : IWindsorInstaller
    {
        private readonly MvcInstallerParameters parameters;

        public MvcControllerInstaller(MvcInstallerParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }
            this.parameters = parameters;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Registers all controllers from the controller assembly.
            container.Register(
                Classes
                    .FromAssembly(parameters.ControllerAssembly)
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
                    .LifestyleSingleton()
                );

            // Register the assembly and namespaces different resource strings are located in. Used by the ResourceController.
            container.Register(
                Component
                    .For<IList<ResourceAssemblyLocation>>()
                    .UsingFactoryMethod(() => parameters.ResourceAssemblies)
                    .LifestyleSingleton()
                );
        }

        private IActionInvoker InstanceActionInvoker(IKernel kernel, ComponentModel model, CreationContext context)
        {
            Type loggerType = context.Handler.ComponentModel.Implementation;
            ILog log = LogManager.GetLogger(loggerType);
            ExceptionHelper exceptionHelper = kernel.Resolve<ExceptionHelper>();
            ChildActionExceptionFilter childActionFilter = new ChildActionExceptionFilter(log, exceptionHelper);
            AjaxTransformFilter ajaxTransform = new AjaxTransformFilter(parameters.ApplicationTitle);
            ActionInvokerFilters filters = parameters.Filters;

            filters.Action.Add(ajaxTransform);
            filters.Exception.Add(childActionFilter);

            return new WindsorActionInvoker(filters);
        }
    }
}
