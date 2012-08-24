using System.Web.Mvc;
using Bruttissimo.Mvc.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bruttissimo.Tests
{
    [TestClass]
    public class ControllerTests
    {
        [TestInitialize]
        public void TestInit()
        {
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
