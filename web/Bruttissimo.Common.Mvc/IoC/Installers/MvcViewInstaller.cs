using System;
using System.IO;
using System.Reflection;
using System.Web;
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
                    .LifestylePerWebRequest()
                );

            container.Register(
                Component
                    .For<MvcResourceHelper>()
                    .UsingFactoryMethod(InstanceMvcResourceHelper)
                    .LifestylePerWebRequest()
                );

            container.Register(
                Component
                    .For<UrlHelper>()
                    .UsingFactoryMethod(InstanceUrlHelper)
                    .LifestyleHybridPerWebRequestPerThread()
                );
        }

        internal MvcResourceHelper InstanceMvcResourceHelper(IKernel kernel, ComponentModel model, CreationContext context)
        {
            string ns = Resources.Constants.ResourceNamespaceRoot;
            HtmlHelper html = context.AdditionalArguments["htmlHelper"] as HtmlHelper;
            MvcResourceHelper helper = new MvcResourceHelper(ns, html, assembly);
            return helper;
        }

        internal UrlHelper InstanceUrlHelper(IKernel kernel, ComponentModel model, CreationContext context)
        {
            HttpContext httpContext = HttpContext.Current;

            if (httpContext == null) // mock it.
            {
                HttpRequest request = new HttpRequest("/", Config.Site.Home, string.Empty);
                HttpResponse response = new HttpResponse(new StringWriter());
                httpContext = new HttpContext(request, response);
            }

            HttpContextWrapper httpContextBase = new HttpContextWrapper(httpContext);
            RouteData routeData = new RouteData();
            RequestContext requestContext = new RequestContext(httpContextBase, routeData);

            return new UrlHelper(requestContext);
        }
    }
}
