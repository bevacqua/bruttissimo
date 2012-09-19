using Bruttissimo.Domain;
using Bruttissimo.Domain.Logic.Email.Model;
using Bruttissimo.Tests.Mocking;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bruttissimo.Tests.Integration
{
    [TestClass]
    public class EmailTests
    {
        private IWindsorContainer container;
        private IEmailService emailService;

        [TestInitialize]
        public void TestInit()
        {
            // Arrange
            container = IntegrationMockHelpers.GetWindsorContainer();
            emailService = container.Resolve<IEmailService>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            container.Dispose();
        }

        // [TestMethod]
        public void EmailService_ShouldSend_RegistrationEmail()
        {
            // Arrange
            RegistrationEmailModel model = new RegistrationEmailModel
            {
                DisplayName = "TestUser"
            };

            // Act
            emailService.SendRegistrationEmail("unit@test.ms", model);

            // if no exceptions are thrown, we're fine.
        }
    }
}
