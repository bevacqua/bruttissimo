using System.Reflection;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Mvc.Interface;
using Bruttissimo.Common.Mvc.SignalR;
using Bruttissimo.Common.Mvc.SignalR.Extensions;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SignalR.Hubs;

namespace Bruttissimo.Common.Mvc.InversionOfControl.Installers
{
    /// <summary>
    /// Registers SignalR components, such as hubs, proxies, or extensions.
    /// </summary>
    internal class SignalRInstaller : IWindsorInstaller
    {
        private readonly Assembly hubAssembly;

        public SignalRInstaller(Assembly hubAssembly)
        {
            Ensure.That(() => hubAssembly).IsNotNull();

            this.hubAssembly = hubAssembly;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component
                    .For(typeof(IHubContextWrapper<>))
                    .ImplementedBy(typeof(HubContextWrapper<>))
                    .LifestyleTransient()
                );

            container.Register(
                Component
                    .For<IJavaScriptMinifier>()
                    .ImplementedBy<HubJavaScriptMinifier>()
                    .LifestyleTransient()
                );

            container.Register(
                Classes
                    .FromAssembly(hubAssembly)
                    .BasedOn<Hub>()
                    .LifestyleTransient()
                );
        }
    }
}
