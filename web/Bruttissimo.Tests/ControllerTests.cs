using System;
using System.Collections;
using System.Web.Mvc;
using Bruttissimo.Common;
using Bruttissimo.Mvc.Controller;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bruttissimo.Tests
{
    [TestClass]
    public class ControllerTests
    {
        [TestInitialize]
        public void TestInit()
        {
            var mock = new Mock<IWindsorContainer>();
            mock.Setup(x=>x.Resolve<>())
            IWindsorContainer container = mock.Object;
            IoC.Register(container);
        }

        [TestMethod]
        public void IndexHomeShouldReturnViewResult()
        {
            // Arrange
            HomeController homeController = new HomeController();

            // Act
            ActionResult result = homeController.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof (ViewResult));
        }
    }
}
