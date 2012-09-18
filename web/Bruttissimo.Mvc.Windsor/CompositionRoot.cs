using Bruttissimo.Common;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Data.Deployment;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using SignalR;

namespace Bruttissimo.Mvc.Windsor
{
    public static class CompositionRoot
    {
        /// <summary>
        /// Registers all dependencies in the composition root, and then runs some start-up processes.
        /// </summary>
        /// <param name="installers"></param>
        public static void Initialize(params IWindsorInstaller[] installers)
        {
            Install(installers);

            UpgradeTool upgradeTool = new UpgradeTool();
            upgradeTool.Execute();

            IJobAutoRunner autoRunner = IoC.Container.Resolve<IJobAutoRunner>();
            autoRunner.Fire();
        }

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
