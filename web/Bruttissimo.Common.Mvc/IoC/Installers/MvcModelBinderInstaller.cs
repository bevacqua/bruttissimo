using System;
using System.Collections.Generic;
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
		private readonly Type type;

		public MvcModelBinderInstaller(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.type = type;
		}

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Classes
					.FromAssemblyContaining(type)
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