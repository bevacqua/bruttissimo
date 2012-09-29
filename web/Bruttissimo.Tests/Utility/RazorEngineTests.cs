using System.IO;
using System.Reflection;
using System.Text;
using Bruttissimo.Common;
using Bruttissimo.Common.Extensions;
using Bruttissimo.Domain.Logic;
using Bruttissimo.Domain.Logic.Email.RazorEngine;
using Bruttissimo.Extensions.RazorEngine;
using Bruttissimo.Tests.Mocking;
using Bruttissimo.Tests.RazorEngine.Model;
using Bruttissimo.Tests.RazorEngine.Template;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RazorEngine.Templating;

namespace Bruttissimo.Tests.Utility
{
    [TestClass]
    public class RazorEngineTests
    {
        private IEmailTemplateService templateService;
        private Assembly assembly;

        [TestInitialize]
        public void TestInit()
        {
            // Arrange
            templateService = MockHelpers.GetRazorTemplateService(typeof (EmailTemplateTests));
            assembly = Assembly.GetAssembly(typeof (EmailTemplateTests));
        }

        [TestMethod]
        public void UnitTestEmailTemplate_ShouldBeAn_AssemblyEmbeddedResource()
        {
            // Act
            Stream stream = assembly.GetManifestResourceStream(typeof (EmailTemplateTests), "UnitTest.cshtml");

            // Assert
            Assert.IsNotNull(stream);
        }

        private const string UnitTestDeclaration = "This is an UnitTest embedded resource.";
        private const string UnitTestNotice = "Please do not remove or change. Sincerely yours, {0}";
        private readonly string UnitTestEmailTemplate = "{0} {1}".FormatWith(UnitTestDeclaration, UnitTestNotice);
        private const string UnitTestModelData = "@Model.Username";

        [TestMethod]
        public void EmbeddedTemplateResolver_ShouldResolve_UnitTestTemplate()
        {
            // Arrange
            ITemplateResolver resolver = new EmbeddedTemplateResolver(typeof (EmailTemplateTests));
            StringBuilder expected = new StringBuilder();
            expected.AppendLine("@model Bruttissimo.Tests.RazorEngine.Model.UnitTestTemplateModel");
            expected.Append(UnitTestEmailTemplate.FormatWith(UnitTestModelData));

            // Act
            string result = resolver.Resolve("UnitTest");

            // Assert
            Assert.AreEqual(expected.ToString(), result);
        }

        [TestMethod]
        public void EmailService_ShouldRun_UnitTestTemplate()
        {
            // Arrange
            UnitTestTemplateModel model = new UnitTestTemplateModel
            {
                Username = "test"
            };

            // Act
            string expected = UnitTestEmailTemplate.FormatWith(model.Username);
            ITemplate template = templateService.Resolve("UnitTest", model);

            string result = template.Run();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EmailService_ShouldRenderLayoutsFromSameAssembly()
        {
            // Arrange
            UnitTestTemplateModel model = new UnitTestTemplateModel
            {
                Username = "test"
            };

            // Act
            string expected = "[{0}]".FormatWith(UnitTestEmailTemplate).FormatWith(model.Username);
            ITemplate template = templateService.Resolve("UnitTestLayout", model);

            string result = template.Run();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EmailService_ShouldRenderLayoutWithSections()
        {
            // Arrange
            UnitTestTemplateModel model = new UnitTestTemplateModel
            {
                Username = "test"
            };

            // Act
            string body = UnitTestDeclaration;
            string section = UnitTestNotice.FormatWith(model.Username);
            string expected = "[\r\n{0}\r\n\r\n\r\n]\r\n{{\r\n\r\n    {1}\r\n\r\n}}".FormatWith(body, section);
            ITemplate template = templateService.Resolve("UnitTestLayoutSection", model);

            string result = template.Run();

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
