using System;
using AutoMapper;
using Castle.MicroKernel;

namespace Bruttissimo.Mvc
{
	public class Mapper : IMapper
	{
		private readonly IMappingEngine engine;
		private readonly IConfiguration configuration;

		public Mapper(IKernel kernel, IMappingEngine engine, IConfiguration configuration, Type[] profileTypes)
		{
			if (kernel == null)
			{
				throw new ArgumentNullException("kernel");
			}
			if (engine == null)
			{
				throw new ArgumentNullException("engine");
			}
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			this.engine = engine;
			this.configuration = configuration;
			this.configuration.ConstructServicesUsing(kernel.Resolve);

			foreach (Type type in profileTypes)
			{
				Profile profile = (Profile)kernel.Resolve(type);
				this.configuration.AddProfile(profile);
			}
		}

		public TDestination Map<TSource, TDestination>(TSource source)
		{
			return engine.Map<TSource, TDestination>(source);
		}
	}
}