using System;
using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Mvc
{
	public class AutoMapperInstaller : IWindsorInstaller
	{
		private readonly Profile[] profiles;

		public AutoMapperInstaller(params Profile[] profiles)
		{
			if (profiles == null)
			{
				throw new ArgumentNullException("profiles");
			}
			this.profiles = profiles;
		}

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			Mapper.Initialize(config =>
			{
				config.ConstructServicesUsing(container.Resolve);

				foreach (Profile profile in profiles)
				{
					config.AddProfile(profile);
				}
			});
		}
	}
}