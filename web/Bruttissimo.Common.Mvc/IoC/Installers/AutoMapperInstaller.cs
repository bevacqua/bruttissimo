using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Mvc
{
	public class AutoMapperInstaller : IWindsorInstaller
	{
		private readonly Assembly profileAssembly;
		private readonly Type[] profileTypes;

		public AutoMapperInstaller(Assembly profileAssembly, params Type[] profileTypes)
		{
			if (profileAssembly == null)
			{
				throw new ArgumentNullException("profileAssembly");
			}
			if (profileTypes == null)
			{
				throw new ArgumentNullException("profileTypes");
			}
			this.profileAssembly = profileAssembly;
			this.profileTypes = profileTypes;
		}

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				AllTypes
					.FromAssembly(profileAssembly)
					.BasedOn(typeof(ITypeConverter<,>))
					.WithServiceSelf()
			);

			container.Register(
				Classes
					.FromAssembly(profileAssembly)
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
					.LifestyleTransient()
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