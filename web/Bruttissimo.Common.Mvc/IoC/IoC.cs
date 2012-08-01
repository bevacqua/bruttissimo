using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.Windsor;
using FluentValidation.Mvc;
using SquishIt.Framework;
using SquishIt.Less;

namespace Bruttissimo.Common.Mvc
{
	public static class IoC
	{
		/// <summary>
		/// Initialize Mvc factories and providers using our custom infrastructure.
		/// </summary>
		public static void Bootstrap(IWindsorContainer container)
		{
			// inject view engine.
			IViewEngine engine = container.Resolve<IViewEngine>();
			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(engine);

			// inject controllers.
			WindsorControllerFactory controllerFactory = new WindsorControllerFactory(container.Kernel);
			ControllerBuilder.Current.SetControllerFactory(controllerFactory);

			// inject model binders.
			WindsorModelBinderProvider binderProvider = container.Resolve<WindsorModelBinderProvider>();
			ModelBinderProviders.BinderProviders.Add(binderProvider);

			// inject model validators.
			WindsorValidatorFactory validatorFactory = new WindsorValidatorFactory(container.Kernel);
			FluentValidationModelValidatorProvider validatorProvider = new FluentValidationModelValidatorProvider(validatorFactory);
			ModelValidatorProviders.Providers.Add(validatorProvider);

			// initialize dotless preprocessors.
			LessPreprocessor dotless = container.Resolve<LessPreprocessor>();
			Bundle.RegisterStylePreprocessor(dotless);
		}
		
		public static IWindsorContainer GetApplicationContainer()
		{
			HttpContext context = HttpContext.Current;
			if (context == null)
			{
				throw new InvalidOperationException(Resources.Error.InvalidContext);
			}
			IContainerAccessor accessor = context.ApplicationInstance as IContainerAccessor;
			if (accessor == null)
			{
				throw new InvalidOperationException(Resources.Error.NoContainerAccessor);
			}
			if (accessor.Container == null)
			{
				throw new InvalidOperationException(Resources.Error.NoContainerInitialized);
			}
			return accessor.Container;
		}

		public static IEnumerable<Type> SelectByInterfaceConvention(Type type, Type[] types)
		{
			Type[] interfaces = type.GetInterfaces();
			foreach (Type interfaceType in interfaces)
			{
				string name = interfaceType.Name;
				if (name.StartsWith("I"))
				{
					name = name.Remove(0, 1);
				}
				if (type.Name.EndsWith(name))
				{
					return new[] { interfaceType };
				}
			}
			return Enumerable.Empty<Type>();
		}

	}
}
