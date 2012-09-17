using System;
using AutoMapper;
using Castle.MicroKernel;

namespace Bruttissimo.Common
{
    public class Mapper : IMapper
    {
        private readonly IMappingEngine engine;

        public Mapper(IKernel kernel, IMappingEngine engine, Type[] profileTypes)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException("kernel");
            }
            if (engine == null)
            {
                throw new ArgumentNullException("engine");
            }
            if (profileTypes == null)
            {
                throw new ArgumentNullException("profileTypes");
            }
            this.engine = engine;

            ConfigureEngine(kernel, profileTypes);
        }

        internal void ConfigureEngine(IKernel kernel, Type[] profileTypes)
        {
            IConfiguration configuration = GetConfiguration();
            configuration.ConstructServicesUsing(kernel.Resolve);

            foreach (Type type in profileTypes)
            {
                Profile profile = (Profile)kernel.Resolve(type);
                configuration.AddProfile(profile);
            }
        }

        private IConfiguration GetConfiguration()
        {
            IMappingEngineRunner runner = engine as IMappingEngineRunner;
            if (runner == null)
            {
                throw new ArgumentException(Resources.Error.AutoMapperInvalidEngine);
            }
            IConfiguration configuration = runner.ConfigurationProvider as IConfiguration;
            if (configuration == null)
            {
                throw new ArgumentException(Resources.Error.AutoMapperInvalidProvider);
            }
            return configuration;
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return engine.Map<TSource, TDestination>(source);
        }

        public IMappingExpression CreateMap(Type sourceType, Type destinationType)
        {
            IConfiguration configuration = GetConfiguration();
            return configuration.CreateMap(sourceType, destinationType);
        }

        public IMappingExpression CreateMap(Type sourceType, Type destinationType, MemberList source)
        {
            IConfiguration configuration = GetConfiguration();
            return configuration.CreateMap(sourceType, destinationType, source);
        }

        public IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>()
        {
            IConfiguration configuration = GetConfiguration();
            return configuration.CreateMap<TSource, TDestination>();
        }

        public IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>(MemberList source)
        {
            IConfiguration configuration = GetConfiguration();
            return configuration.CreateMap<TSource, TDestination>(source);
        }
    }
}
