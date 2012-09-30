using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Domain.Logic.MiniMembership;
using Bruttissimo.Domain.Repository;
using Bruttissimo.Tests.Mocking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bruttissimo.Tests
{
    [TestClass]
    public class MembershipTests
    {
        private MiniMembershipProvider miniMembership;

        [TestInitialize]
        public void TestInit()
        {
            // Arrange
            Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
            User user = new User
            {
                Email = "test",
                Password = "123"
            };

            userRepository.Setup(x => x
                                          .GetByEmail("test"))
                .Returns(user);

            userRepository.Setup(x => x
                                          .AreMatchingPasswords(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string l, string r) => l == r);

            miniMembership = MockHelpers.FakeMiniMembership(userRepository.Object);
        }

        [TestMethod]
        public void ValidateUser_WithInvalidUser_ReturnsFalse()
        {
            // Act
            bool valid = miniMembership.ValidateUser("invalid", "123");

            // Assert
            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void ValidateUser_WithInvalidPassword_ReturnsFalse()
        {
            // Act
            bool valid = miniMembership.ValidateUser("test", "invalid");

            // Assert
            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void ValidateUser_WithValidUserAndPassword_ReturnsTrue()
        {
            // Act
            bool valid = miniMembership.ValidateUser("test", "123");

            // Assert
            Assert.IsTrue(valid);
        }
    }
}
