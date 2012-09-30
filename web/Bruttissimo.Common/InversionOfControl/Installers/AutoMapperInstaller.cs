using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using AutoMapper.Mappers;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Interface;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Mapper = Bruttissimo.Common.AutoMapper.Mapper;

namespace Bruttissimo.Common.InversionOfControl.Installers
{
    internal sealed class AutoMapperInstaller : IWindsorInstaller
    {
        private readonly Assembly[] mapperAssemblies;

        public AutoMapperInstaller(params Assembly[] mapperAssemblies)
        {
            Ensure.That(() => mapperAssemblies).IsNotNull();
            this.mapperAssemblies = mapperAssemblies;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            foreach (Assembly assembly in mapperAssemblies)
            {
                container.Register(
                    Classes
                        .FromAssembly(assembly)
                        .BasedOn<IMapperConfigurator>()
                        .WithServiceFromInterface(typeof(IMapperConfigurator))
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
                        (k, parameters) => parameters["configurators"] = container.ResolveAll<IMapperConfigurator>()
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
