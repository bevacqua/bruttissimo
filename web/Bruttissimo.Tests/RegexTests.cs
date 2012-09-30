using System.Text.RegularExpressions;
using Bruttissimo.Common.Resources;
using Bruttissimo.Common.Static;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bruttissimo.Tests
{
    [TestClass]
    public class RegexTests
    {
        [TestInitialize]
        public void TestInit()
        {
        }

        [TestMethod]
        public void JavaScriptViewNamingConventionRegex_ShouldMatchViewPath()
        {
            // Arrange
            Regex regex = CompiledRegex.JavaScriptViewNamingConvention;
            const string input = "~/Views/User/Register.cshtml";

            // Act
            bool result = regex.IsMatch(input);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void JavaScriptViewNamingConventionRegex_ReplacesViewNamesProperly()
        {
            // Arrange
            Regex regex = CompiledRegex.JavaScriptViewNamingConvention;
            string replacement = Regular.JavaScriptViewNamingExtension;
            const string input = "~/Views/User/Register.cshtml";
            const string expected = "~/Views/User/Register.js.cshtml";

            // Act
            string result = regex.Replace(input, replacement);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
