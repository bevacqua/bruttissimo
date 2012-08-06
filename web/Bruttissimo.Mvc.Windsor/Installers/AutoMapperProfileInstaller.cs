using AutoMapper;
using Bruttissimo.Mvc.Model;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Mvc
{
	public class AutoMapperProfileInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			Profile entityToViewModel = container.Resolve<EntityToViewModelProfile>();
			Profile[] profiles = new[] { entityToViewModel };
			container.Install(new AutoMapperInstaller(profiles));
		}
	}
}