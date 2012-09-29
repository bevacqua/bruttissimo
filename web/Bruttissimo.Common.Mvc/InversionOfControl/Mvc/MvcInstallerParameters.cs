using System;
using System.Collections.Generic;
using System.Reflection;
using Bruttissimo.Common.Guard;

namespace Bruttissimo.Common.Mvc
{
    public sealed class MvcInstallerParameters
    {
        public Assembly HubAssembly { get; private set; }
        public Assembly ModelAssembly { get; private set; }
        public Assembly ViewAssembly { get; private set; }
        public Assembly ControllerAssembly { get; private set; }
        public string ApplicationTitle { get; private set; }
        public IList<ResourceAssemblyLocation> ResourceAssemblies { get; private set; }
        public ActionInvokerFilters Filters { get; private set; }
        public Assembly JobAssembly { get; set; }
        public Assembly[] MapperAssemblies { get; set; }

        /// <summary>
        /// All required parameters for the Mvc infrastructure package.
        /// </summary>
        /// <param name="modelAssembly">The model assembly.</param>
        /// <param name="viewAssembly">The view assembly.</param>
        /// <param name="controllerAssembly">The controller assembly.</param>
        /// <param name="applicationTitle">The default title to display in ajax requests when partially rendering a view.</param>
        /// <param name="resourceAssemblies">The location of the different string resources that are rendered client-side.</param>
        /// <param name="filters">A list of default action invoker filters.</param>
        /// <param name="jobAssembly">The assembly containing jobs.</param>
        /// <param name="automapperAssemblies">A list of AutoMapper profile types.</param>
        /// <param name="hubAssembly">The SignalR hub assembly.</param>
        public MvcInstallerParameters(
            Assembly modelAssembly,
            Assembly viewAssembly,
            Assembly controllerAssembly,
            string applicationTitle,
            IList<ResourceAssemblyLocation> resourceAssemblies,
            ActionInvokerFilters filters,
            Assembly jobAssembly,
            Assembly[] automapperAssemblies,
            Assembly hubAssembly)
        {
            Ensure.That(modelAssembly, "modelAssembly").IsNotNull();
            Ensure.That(viewAssembly, "viewAssembly").IsNotNull();
            Ensure.That(controllerAssembly, "controllerAssembly").IsNotNull();
            Ensure.That(applicationTitle, "applicationTitle").IsNotNullOrEmpty();
            Ensure.That(resourceAssemblies, "resourceAssemblies").IsNotNull();
            Ensure.That(filters, "filters").IsNotNull();
            Ensure.That(jobAssembly, "jobAssembly").IsNotNull();
            Ensure.That(filters, "filters").IsNotNull();
            Ensure.That(automapperAssemblies, "automapperAssemblies").IsNotNull();
            Ensure.That(hubAssembly, "hubAssembly").IsNotNull();

            ModelAssembly = modelAssembly;
            ViewAssembly = viewAssembly;
            ControllerAssembly = controllerAssembly;
            ApplicationTitle = applicationTitle;
            ResourceAssemblies = resourceAssemblies;
            Filters = filters;
            JobAssembly = jobAssembly;
            MapperAssemblies = automapperAssemblies;
            HubAssembly = hubAssembly;
        }
    }
}
