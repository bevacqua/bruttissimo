using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Common.Mvc
{
	/// <summary>
	/// Registers all model binders and the model binder provider.
	/// </summary>
	public sealed class MvcModelBinderInstaller : IWindsorInstaller
	{
		private readonly Assembly assembly;

		public MvcModelBinderInstaller(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			this.assembly = assembly;
		}

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Classes
					.FromAssembly(assembly)
					.BasedOn<IModelBinder>()
					.LifestylePerWebRequest()
			);

			container.Register(
			   Component
				   .For<WindsorModelBinderProvider>()
				   .UsingFactoryMethod(InstanceModelBinderProvider)
				   .LifestyleTransient()
			);
		}

		private WindsorModelBinderProvider InstanceModelBinderProvider(IKernel kernel)
		{
			IDictionary<Type, Type> modelBinderTypes = new Dictionary<Type, Type>();
			IHandler[] handlers = kernel.GetAssignableHandlers(typeof(IModelBinder));
			foreach (IHandler handler in handlers)
			{
				Type modelBinderType = handler.ComponentModel.Implementation;
				ModelTypeAttribute modelTypeAttribute = modelBinderType.GetAttribute<ModelTypeAttribute>();
				if (modelTypeAttribute == null)
				{
					throw new ArgumentException(Resources.Error.ModelTypeAttributeMissing.FormatWith(modelBinderType.FullName));
				}
				modelBinderTypes.Add(modelTypeAttribute.ModelType, modelBinderType);
			}
			return new WindsorModelBinderProvider(kernel, modelBinderTypes);
		}
	}
}