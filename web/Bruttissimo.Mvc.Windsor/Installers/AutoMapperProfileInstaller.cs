using System;
using System.Reflection;
using AutoMapper;
using Bruttissimo.Mvc.Model;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Mvc
{
	public class AutoMapperProfileInstaller : IWindsorInstaller
	{
		private readonly Assembly modelAssembly;

		public AutoMapperProfileInstaller(Assembly modelAssembly)
		{
			if (modelAssembly == null)
			{
				throw new ArgumentNullException("modelAssembly");
			}
			this.modelAssembly = modelAssembly;
		}

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			Type entityToViewModel = typeof(EntityToViewModelProfile);
			Type[] profileTypes = new[] { entityToViewModel };

			container.Install(
				new AutoMapperInstaller(modelAssembly, profileTypes)
			);
		}
	}
}