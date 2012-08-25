using Bruttissimo.Common;
using Bruttissimo.Common.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using SignalR;

namespace Bruttissimo.Mvc.Windsor
{
    public static class CompositionRoot
    {
        public static void Install(params IWindsorInstaller[] installers)
        {
            IWindsorContainer container = new WindsorContainer();
            container.Install(installers);
            IoC.Register(container);

            MvcInfrastructure.Initialize(container.Kernel);

            GlobalHost.DependencyResolver = new WindsorDependencyResolver(container.Kernel);
        }
    }
}
