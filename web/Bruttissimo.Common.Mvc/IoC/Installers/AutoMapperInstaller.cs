using System;
using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Mvc
{
	public class AutoMapperInstaller : IWindsorInstaller
	{
		private readonly Type[] profileTypes;

		public AutoMapperInstaller(params Type[] profileTypes)
		{
			if (profileTypes == null)
			{
				throw new ArgumentNullException("profileTypes");
			}
			this.profileTypes = profileTypes;
		}

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			Mapper.Initialize(config =>
			{
				config.ConstructServicesUsing(container.Resolve);

				foreach (Type type in profileTypes)
				{
					Profile profile = (Profile)container.Resolve(type);
					config.AddProfile(profile);
				}
			});
		}
	}
}