using System;
using System.Collections.Generic;
using System.Reflection;

namespace Bruttissimo.Common.Mvc
{
	public sealed class MvcInstallerParameters
	{
		public Assembly ModelAssembly { get; private set; }
		public Assembly ViewAssembly { get; private set; }
		public Assembly ControllerAssembly { get; private set; }
		public string ApplicationTitle { get; private set; }
		public IList<ResourceAssemblyLocation> ResourceAssemblies { get; private set; }
		public ActionInvokerFilters Filters { get; private set; }

		/// <summary>
		/// All required parameters for the Mvc infrastructure package.
		/// </summary>
		/// <param name="modelAssembly">The model assembly.</param>
		/// <param name="viewAssembly">The view assembly.</param>
		/// <param name="controllerAssembly">The controller assembly.</param>
		/// <param name="applicationTitle">The default title to display in ajax requests when partially rendering a view.</param>
		/// <param name="resourceAssemblies">The location of the different string resources that are rendered client-side.</param>
		/// <param name="filters">A list of default action invoker filters.</param>
		public MvcInstallerParameters(
			Assembly modelAssembly,
			Assembly viewAssembly,
			Assembly controllerAssembly,
			string applicationTitle,
			IList<ResourceAssemblyLocation> resourceAssemblies,
			ActionInvokerFilters filters)
		{
			if (modelAssembly == null)
			{
				throw new ArgumentNullException("modelAssembly");
			}
			if (viewAssembly == null)
			{
				throw new ArgumentNullException("viewAssembly");
			}
			if (controllerAssembly == null)
			{
				throw new ArgumentNullException("controllerAssembly");
			}
			if (applicationTitle == null)
			{
				throw new ArgumentNullException("applicationTitle");
			}
			if (resourceAssemblies == null)
			{
				throw new ArgumentNullException("resourceAssemblies");
			}
			if (filters == null)
			{
				throw new ArgumentNullException("filters");
			}
			ModelAssembly = modelAssembly;
			ViewAssembly = viewAssembly;
			ControllerAssembly = controllerAssembly;
			ApplicationTitle = applicationTitle;
			ResourceAssemblies = resourceAssemblies;
			Filters = filters;
		}
	}
}