using System.Collections.Generic;
using System.Reflection;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Common.Resources;
using Bruttissimo.Mvc.Windsor;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Mvc
{
	internal class ApplicationInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			Assembly viewAssembly = typeof(ApplicationInstaller).Assembly;
			
			string title = Views.Shared.Resources.Application.Title;

			IList<ResourceAssemblyLocation> locations = new List<ResourceAssemblyLocation>
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