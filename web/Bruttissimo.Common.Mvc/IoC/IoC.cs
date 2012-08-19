using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Castle.Windsor;
using FluentValidation.Mvc;
using Quartz;
using SquishIt.Framework;
using SquishIt.Less;

namespace Bruttissimo.Common.Mvc
{
    public static class IoC
    {
        private static IContainerAccessor Accessor;

        /// <summary>
        /// Uses IoC as a Service Locator, should be used as a last resort.
        /// </summary>
        public static IWindsorContainer Container
        {
            get
            {
                if (Accessor == null)
                {
                    throw new InvalidOperationException(Resources.Error.NoContainerInitialized);
                }
                return Accessor.Container;
            }
        }

        /// <summary>
        /// Initialize Mvc factories and providers using our custom infrastructure.
        /// </summary>
        public static void Bootstrap(IWindsorContainer container)
        {
            ContainerAccessor accessor = new ContainerAccessor(container);
            Accessor = accessor;

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

            // auto run jobs on application start.
            IScheduler scheduler = container.Resolve<IScheduler>();
            IJobAutoRunner autoRunner = container.Resolve<IJobAutoRunner>();
            autoRunner.Fire(scheduler);
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
                    return new[] {interfaceType};
                }
            }
            return Enumerable.Empty<Type>();
        }

        public static void Shutdown()
        {
            if (Container != null)
            {
                Container.Dispose();
            }
        }
    }
}
