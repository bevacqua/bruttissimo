using System;
using System.Collections.Generic;
using AutoMapper;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Common.Mvc
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
			container.Register(
				AllTypes
                    .From(profileTypes)
					.BasedOn(typeof(ITypeConverter<,>))
					.WithServiceSelf()
			);

			container.Register(
				Classes
                    .From(profileTypes)
					.BasedOn<Profile>()
					.LifestyleTransient()
			);

			container.Register(
				Component
					.For<ITypeMapFactory>()
					.ImplementedBy<TypeMapFactory>()
					.LifestyleTransient()
			);

			container.Register(
				Component
					.For<IConfiguration, IConfigurationProvider>()
					.UsingFactoryMethod(InstanceConfigurationStore)
					.LifestyleTransient()
			);

			container.Register(
				Component
					.For<IMappingEngine>()
					.ImplementedBy<MappingEngine>()
					.LifestyleTransient()
			);

			container.Register(
				Component
					.For<IMapper>()
					.ImplementedBy<Mapper>()
					.DynamicParameters(
						(k, parameters) => parameters["profileTypes"] = profileTypes
					)
					.LifestyleSingleton()
			);
		}

		private ConfigurationStore InstanceConfigurationStore(IKernel kernel)
		{
			ITypeMapFactory typeMapFactory = kernel.Resolve<ITypeMapFactory>();
			IEnumerable<IObjectMapper> mappers = AutoMapper.Mappers.MapperRegistry.AllMappers();

			return new ConfigurationStore(typeMapFactory, mappers);
		}
	}
}