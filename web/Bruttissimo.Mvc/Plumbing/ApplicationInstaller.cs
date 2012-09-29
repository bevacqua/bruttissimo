using System.Collections.Generic;
using System.Reflection;
using Bruttissimo.Common.Mvc.Core.Models;
using Bruttissimo.Common.Resources;
using Bruttissimo.Mvc.Views.Shared.Resources;
using Bruttissimo.Mvc.Windsor.Installers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Mvc.Plumbing
{
    internal class ApplicationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            Assembly viewAssembly = typeof(ApplicationInstaller).Assembly;

            string title = Application.Title;

            var locations = new List<ResourceAssemblyLocation>
            {
                new ResourceAssemblyLocation
                {
                    Assembly = viewAssembly,
                    Namespace = Constants.JavaScriptResourceNamespaceRoot
                }
            };
            WindsorInstaller installer = new WindsorInstaller(viewAssembly, title, locations);
            container.Install(installer);
        }
    }
}
