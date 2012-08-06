using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Bruttissimo.Common.Mvc;
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
				Classes
					.FromAssembly(profileAssembly)
					.BasedOn<Profile>()
					.LifestyleTransient()
			);

			container.Register(
				Component
					.For<ITypeMapFactory>()
					.ImplementedBy<TypeMapFactory>()
			);

			Property mappers = Property
				.ForKey<IEnumerable<IObjectMapper>>()
				.Eq(AutoMapper.Mappers.MapperRegistry.AllMappers);

			container.Register(
				Component
					.For<IConfiguration, IConfigurationProvider>()
					.ImplementedBy<ConfigurationStore>()
					.DependsOn(new[] { mappers })
			);

			container.Register(
				Component
					.For<IMappingEngine>()
					.ImplementedBy<MappingEngine>()
			);

			Property profiles = Property
				.ForKey<Type[]>()
				.Eq(profileTypes);

			container.Register(
				AllTypes
					.FromAssemblyContaining<IMapper>()
					.BasedOn<IMapper>()
					.WithServiceSelect(IoC.SelectByInterfaceConvention)
					.Configure(x => x.DependsOn(new[] { profiles }))
			);
		}
	}
}