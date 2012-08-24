using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Logic;
using Bruttissimo.Mvc.Controller;
using Bruttissimo.Mvc.Model;
using Bruttissimo.Tests.Mocking;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Handlers;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Diagnostics;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bruttissimo.Tests.Integration
{
    [TestClass]
    public class InversionOfControlTests
    {
        private IWindsorContainer container;

        [TestInitialize]
        public void TestInit()
        {
            container = IntegrationMockHelpers.GetWindsorContainer();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            container.Dispose();
        }

        [TestMethod]
        public void ContainerShouldResolve_MiniPrincipalModelBinder()
        {
            ContainerShouldResolve<MiniPrincipalModelBinder>();
        }

        [TestMethod]
        public void ContainerShouldResolve_ModelBinderProvider()
        {
            ContainerShouldResolve<WindsorModelBinderProvider>();
        }

        [TestMethod]
        public void ContainerShouldResolve_MembershipProvider()
        {
            ContainerShouldResolve<IMembershipProvider>();
        }

        [TestMethod]
        public void ContainerShouldResolve_FormsAuthentication()
        {
            ContainerShouldResolve<IFormsAuthentication>();
        }

        [TestMethod]
        public void ContainerShouldResolve_ActionInvoker()
        {
            IActionInvoker result = container.Resolve<IActionInvoker>();
            Assert.IsInstanceOfType(result, typeof (IActionInvoker));
        }

        [TestMethod]
        public void ContainerShouldResolve_LinkServiceLazily()
        {
            ContainerShouldResolve<Lazy<LinkService>>();
        }

        [TestMethod]
        public void ContainerShouldResolve_UserRepository()
        {
            ContainerShouldResolve<IUserRepository>();
        }

        [TestMethod]
        public void ContainerShouldResolve_HomeController()
        {
            ContainerShouldResolve<HomeController>();
        }

        [TestMethod]
        public void ContainerShouldResolve_UserController()
        {
            ContainerShouldResolve<UserController>();
        }

        [TestMethod]
        public void ContainerShouldResolve_PostController()
        {
            ContainerShouldResolve<PostsController>();
        }

        [TestMethod]
        public void ContainerShouldResolve_LinkService()
        {
            ContainerShouldResolve<ILinkService>();
        }

        [TestMethod]
        public void ContainerShouldResolve_LinkCrawler()
        {
            ContainerShouldResolve<ILinkCrawlerService>();
        }

        [TestMethod]
        public void ContainerShouldResolve_ImageStorageRepository()
        {
            ContainerShouldResolve<IPictureStorageRepository>();
        }

        [TestMethod]
        public void ContainerShouldResolve_EmailRepository()
        {
            ContainerShouldResolve<IEmailRepository>();
        }

        [TestMethod]
        public void ContainerShouldResolve_EmailTemplateService()
        {
            ContainerShouldResolve<IEmailTemplateService>();
        }

        [TestMethod]
        public void ContainerShouldResolve_EmailTemplateServiceInAConstructor()
        {
            container.Register(
                Component
                    .For<TestTemplateServiceInConstructor>()
                    .ImplementedBy<TestTemplateServiceInConstructor>()
                );
            ContainerShouldResolve<TestTemplateServiceInConstructor>();
        }

        [TestMethod]
        public void ContainerShouldResolve_DynamicFluentValidator()
        {
            ContainerShouldResolve<IValidator<dynamic>>();
        }

        public void ContainerShouldResolve<T>()
        {
            T result = container.Resolve<T>();
            Assert.IsInstanceOfType(result, typeof (T));
        }

        #region General Container Configuration and Lazy Loading special case

        [TestMethod]
        public void ContainerShould_BeProperlyConfigured() // TODO: same test but turning lazyloaded types into the actual types :)
        {
            // Arrange
            IDiagnosticsHost host = (IDiagnosticsHost)container.Kernel.GetSubSystem(SubSystemConstants.DiagnosticsKey);
            IPotentiallyMisconfiguredComponentsDiagnostic diagnostics = host.GetDiagnostic<IPotentiallyMisconfiguredComponentsDiagnostic>();
            StringBuilder message = new StringBuilder();

            // Act
            IHandler[] handlers = diagnostics.Inspect();

            foreach (AbstractHandler handler in handlers)
            {
                // work around Windsor being unable to predict whether a dependency could be resolved taking into account component loaders.
                DependencyModel[] dependencies = handler.ComponentModel.Dependencies
                    .OfType<ConstructorDependencyModel>()
                    .Cast<DependencyModel>()
                    .Where(TypeIsLazilyLoaded)
                    .ToArray();

                // if both messages are equal, it would mean only Lazy<T> dependencies are the only issue in this case.
                string lazy = InspectContainerScenario(handler, dependencies);
                string assertion = InspectContainerScenario(handler);

                if (assertion != lazy)
                {
                    message.Append(assertion);
                }
            }

            // Assert
            Assert.IsTrue(message.Length == 0, message.ToString());
        }

        internal string InspectContainerScenario(AbstractHandler handler, DependencyModel[] dependencies = null)
        {
            StringBuilder message = new StringBuilder();
            DependencyInspector inspector = new DependencyInspector(message);
            if (dependencies == null)
            {
                handler.ObtainDependencyDetails(inspector);
            }
            else if (dependencies.Any())
            {
                inspector.Inspect(handler, dependencies, container.Kernel);
            }
            return message.ToString();
        }

        internal bool TypeIsLazilyLoaded(DependencyModel model)
        {
            Type type = model.TargetType;
            bool lazy = type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Lazy<>);
            return lazy;
        }

        #endregion
    }
}
