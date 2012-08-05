using AutoMapper;
using Bruttissimo.Mvc.Model;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Mvc
{
	public class AutoMapperInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			Mapper.Initialize(config =>
			{
				config.ConstructServicesUsing(container.Resolve);
				config.AddProfile<EntityToViewModelProfile>();
			});
		}
	}
}