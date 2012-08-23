using System;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
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
    internal sealed class MvcViewInstaller : IWindsorInstaller
    {
        private readonly Assembly assembly;

        public MvcViewInstaller(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }
            this.assembly = assembly;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Register the razor view engine.
            container.Register(
                Component
                    .For<IViewEngine>()
                    .ImplementedBy<ExtendedViewEngine>()
                    .LifestyleSingleton()
                );

            // Register view helpers.
            container.Register(
                Component
                    .For<JavaScriptHelper>()
                    .ImplementedBy<JavaScriptHelper>()
                    .LifestyleTransient()
                );

            container.Register(
                Component
                    .For<MvcResourceHelper>()
                    .UsingFactoryMethod(InstanceMvcResourceHelper)
                    .LifestyleTransient()
                );

            container.Register(
                Component
                    .For<UrlHelper>()
                    .UsingFactoryMethod(InstanceUrlHelper)
                    .LifestyleTransient()
                );
        }

        internal MvcResourceHelper InstanceMvcResourceHelper(IKernel kernel, ComponentModel model, CreationContext context)
        {
            string ns = Resources.Constants.ResourceNamespaceRoot;
            HtmlHelper html = context.AdditionalArguments["htmlHelper"] as HtmlHelper;
            MvcResourceHelper helper = new MvcResourceHelper(ns, html, assembly);
            return helper;
        }

        internal UrlHelper InstanceUrlHelper(IKernel kernel)
        {
            RequestContext requestContext = kernel.Resolve<RequestContext>();
            UrlHelper helper = new UrlHelper(requestContext);
            return helper;
        }
    }
}
