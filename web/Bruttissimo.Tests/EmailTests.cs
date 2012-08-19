using System;
using Bruttissimo.Domain;
using Bruttissimo.Tests.Mocking;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bruttissimo.Tests
{
    [TestClass]
    public class EmailTests
    {
        private IEmailService emailService;

        [TestInitialize]
        public void TestInit()
        {
            // Arrange
            emailService = MockHelpers.FakeEmailService();
        }

        [TestMethod]
        public void EmailService_ShouldSend_UnitTestEmail()
        {
            // Act
            emailService.SendEmail("unit@test.ms", "Unit Test Subject", "This is the message body.");

            // if no exceptions are thrown, we're fine.
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void EmailService_ShouldThrowIfNullWhen_GettingEmailBody()
        {
            // Act
            emailService.RunEmailTemplate(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void EmailService_ShouldThrowIfNullWhen_SendingEmail()
        {
            // Act
            emailService.SendEmail(null, null, null);
        }
    }
}
