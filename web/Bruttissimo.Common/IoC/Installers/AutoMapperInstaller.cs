using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using AutoMapper.Mappers;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Common
{
    internal sealed class AutoMapperInstaller : IWindsorInstaller
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
            IEnumerable<Assembly> assemblies = profileTypes.Select(t => t.Assembly).ToList();

            foreach (Assembly assembly in assemblies)
            {
                container.Register(
                    AllTypes
                        .FromAssembly(assembly)
                        .BasedOn(typeof (ITypeConverter<,>))
                        .WithServiceSelf()
                    );
            }

            foreach (Assembly assembly in assemblies)
            {
                container.Register(
                    Classes
                        .FromAssembly(assembly)
                        .BasedOn<Profile>()
                        .LifestyleTransient()
                    );
            }

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
            IEnumerable<IObjectMapper> mappers = MapperRegistry.AllMappers();

            return new ConfigurationStore(typeMapFactory, mappers);
        }
    }
}
