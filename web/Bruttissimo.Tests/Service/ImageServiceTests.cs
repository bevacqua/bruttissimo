using System;
using Bruttissimo.Domain;
using Bruttissimo.Tests.Mocking;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bruttissimo.Tests
{
    [TestClass]
    public class ImageServiceTests
    {
        private IPictureService pictureService;

        [TestInitialize]
        public void TestInit()
        {
            pictureService = MockHelpers.FakeImageService();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void SizeAndSaveImage_WithNullName_ThrowsArgumentNullException()
        {
            // Act
            pictureService.SizeAndSaveImage(null, null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void SizeAndSaveImage_WithNullImage_ThrowsArgumentNullException()
        {
            // Act
            pictureService.SizeAndSaveImage(string.Empty, null, 0);
        }
    }
}
