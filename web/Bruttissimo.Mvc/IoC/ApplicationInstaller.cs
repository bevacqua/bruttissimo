using System;
using System.Collections.Generic;
using Bruttissimo.Common.Resources;
using Bruttissimo.Mvc.Controllers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Mvc
{
	internal class ApplicationInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			Type type = typeof(ApplicationInstaller);
			string title = Views.Shared.Resources.Application.Title;

			IList<ResourceAssemblyLocation> locations = new List<ResourceAssemblyLocation>
            {
                new ResourceAssemblyLocation
                {
                    Assembly = type.Assembly,
                    Namespace = Constants.JavaScriptResourceNamespaceRoot
                }
            };
			WindsorInstaller installer = new WindsorInstaller(type, title, locations);
			container.Install(installer);
		}
	}
}