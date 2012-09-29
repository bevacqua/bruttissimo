using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Mvc.Core.ActionFilters;
using Bruttissimo.Common.Mvc.Core.ErrorHandling;
using Bruttissimo.Common.Mvc.Core.Models;
using Bruttissimo.Common.Mvc.InversionOfControl.Mvc;
using Bruttissimo.Common.Mvc.Utility;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using StackExchange.Profiling.MVCHelpers;
using log4net;

namespace Bruttissimo.Common.Mvc.InversionOfControl.Installers
{
    /// <summary>
    /// Registers core MVC-specific dependencies.
    /// </summary>
    internal sealed class MvcControllerInstaller : IWindsorInstaller
    {
        private readonly MvcInstallerParameters parameters;

        public MvcControllerInstaller(MvcInstallerParameters parameters)
        {
            Ensure.That(parameters, "parameters").IsNotNull();

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
            ActionInvokerFilters filters = parameters.Filters;

            AjaxTransformFilter ajaxTransform = new AjaxTransformFilter(parameters.ApplicationTitle);
            ProfilingActionFilter profiler = new ProfilingActionFilter();

            Type loggerType = context.Handler.ComponentModel.Implementation;
            ILog log = LogManager.GetLogger(loggerType);
            ExceptionHelper exceptionHelper = kernel.Resolve<ExceptionHelper>();
            ChildActionExceptionFilter childActionFilter = new ChildActionExceptionFilter(log, exceptionHelper);
            
            filters.Action.Add(ajaxTransform);
            filters.Action.Add(profiler);

            filters.Exception.Add(childActionFilter);

            return new WindsorActionInvoker(filters);
        }
    }
}
