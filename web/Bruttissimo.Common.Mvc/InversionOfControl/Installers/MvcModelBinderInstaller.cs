using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using Bruttissimo.Common.Extensions;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Helpers;
using Bruttissimo.Common.Mvc.InversionOfControl.Mvc;
using Bruttissimo.Common.Resources;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Common.Mvc.InversionOfControl.Installers
{
    /// <summary>
    /// Registers all model binders and the model binder provider.
    /// </summary>
    internal sealed class MvcModelBinderInstaller : IWindsorInstaller
    {
        private readonly Assembly assembly;

        public MvcModelBinderInstaller(Assembly assembly)
        {
            Ensure.That(assembly, "assembly").IsNotNull();

            this.assembly = assembly;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes
                    .FromAssembly(assembly)
                    .BasedOn<IModelBinder>()
                    .LifestyleTransient()
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
                    throw new ArgumentException(Error.ModelTypeAttributeMissing.FormatWith(modelBinderType.FullName));
                }
                modelBinderTypes.Add(modelTypeAttribute.ModelType, modelBinderType);
            }
            return new WindsorModelBinderProvider(kernel, modelBinderTypes);
        }
    }
}
