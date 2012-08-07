using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace Bruttissimo.Common.Mvc
{
	/// <summary>
	/// Registers dependencies for Quartz.NET components.
	/// </summary>
	public sealed class QuartzInstaller : IWindsorInstaller
	{
		private readonly Assembly jobAssembly;

		public QuartzInstaller(Assembly jobAssembly)
		{
			if (jobAssembly == null)
			{
				throw new ArgumentNullException("jobAssembly");
			}
			this.jobAssembly = jobAssembly;
		}

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			// Register the Job Factory.
			container.Register(
				Component
					.For<IJobFactory>()
					.ImplementedBy<WindsorJobFactory>()
			);

			// Register Job Scheduler Factory.
			container.Register(
				Component
					.For<ISchedulerFactory>()
					.ImplementedBy<StdSchedulerFactory>()
			);

			// Register Job Scheduler.
			container.Register(
				Component
					.For<IScheduler>()
					.UsingFactory((ISchedulerFactory f) => f.GetScheduler())
			);

			// Register Job Scheduler.
			container.Register(
				Component
					.For<IJobAutoRunner>()
					.UsingFactoryMethod(InstanceJobAutoRunner)
			);

			// Register all jobs in target assembly.
			container.Register(
				AllTypes
					.FromAssembly(jobAssembly)
					.BasedOn<IJob>()
					.WithServiceSelf()
					.LifestyleTransient()
			);
		}

		/// <summary>
		/// Gets all jobs marked as AutoRun in the target assembly.
		/// </summary>
		private IEnumerable<Type> FindAutoRunJobTypes()
		{
			Type jobType = typeof(IJob);

			IEnumerable<Type> jobs = jobAssembly
				.GetTypes()
				.Where(jobType.IsAssignableFrom)
				.Where(type => type.HasAttribute<AutoRunAttribute>());

			return jobs;
		}

		internal IJobAutoRunner InstanceJobAutoRunner(IKernel kernel)
		{
			IList<Type> jobTypes = FindAutoRunJobTypes().ToList();
			IJobAutoRunner autoRunner = new JobAutoRunner(jobTypes);
			return autoRunner;
		}
	}
}