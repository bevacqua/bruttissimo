using System;
using System.Drawing;
using Bruttissimo.Domain.Logic.Repository;
using Bruttissimo.Tests.Mocking;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bruttissimo.Tests.Repository
{
    [TestClass]
    public class FsImageStorageRepositoryTests
    {
        private PictureStorageRepository pictureStorageRepository;

        [TestInitialize]
        public void TestInit()
        {
            pictureStorageRepository = MockHelpers.FakeImageStorageRepository();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Save_WithNullImage_ThrowsArgumentNullException()
        {
            // Act
            pictureStorageRepository.Save(null, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Save_WithNullName_ThrowsArgumentNullException()
        {
            // Act
            pictureStorageRepository.Save(new Bitmap(1, 1), null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Load_WithNullName_ThrowsArgumentNullException()
        {
            // Act
            pictureStorageRepository.Load(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetPhysicalPath_WithNullName_ThrowsArgumentNullException()
        {
            // Act
            pictureStorageRepository.GetPhysicalPath(null);
        }

        private const string ImageId = "ImageId";

        [TestMethod]
        public void GetPhysicalPath_WithId_IsNotNullOrEmpty()
        {
            // Act
            string physicalPath = pictureStorageRepository.GetPhysicalPath(ImageId);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(physicalPath));
        }

        [TestMethod]
        public void GetPhysicalPath_WithId_EndsWithIdAndExtension()
        {
            // Arrange
            string filename = string.Concat(ImageId, ".jpg");

            // Act
            string physicalPath = pictureStorageRepository.GetPhysicalPath(ImageId);

            // Assert
            Assert.IsTrue(physicalPath.EndsWith(filename));
        }
    }
}
