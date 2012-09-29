using Bruttissimo.Common;
using Bruttissimo.Common.InversionOfControl;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Common.Mvc.InversionOfControl;
using Bruttissimo.Common.Mvc.InversionOfControl.SignalR;
using Bruttissimo.Common.Quartz;
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
        public static void Initialize(params IWindsorInstaller[] installers)
        {
            Install(installers);

            UpgradeTool upgradeTool = new UpgradeTool();
            upgradeTool.Execute(); // database script changes.

            IJobAutoRunner autoRunner = IoC.Container.Resolve<IJobAutoRunner>();
            autoRunner.Fire(); // AutoRun Quartz jobs.
        }

        internal static void Install(params IWindsorInstaller[] installers)
        {
            IWindsorContainer container = new WindsorContainer();
            container.Install(installers);
            IoC.Register(container);

            MvcInfrastructure.Initialize(container.Kernel);

            GlobalHost.DependencyResolver = new WindsorDependencyResolver(container.Kernel);
        }
    }
}
