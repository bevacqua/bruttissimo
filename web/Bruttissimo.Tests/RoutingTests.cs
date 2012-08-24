using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;
using Bruttissimo.Common;
using Bruttissimo.Mvc;
using Bruttissimo.Tests.Mocking;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bruttissimo.Tests
{
    [TestClass]
    public class RoutingTests
    {
        [TestInitialize]
        public void TestInit()
        {
            // Arrange
            _routes = new RouteCollection();
            Routing.RegisterRoutes(_routes);
        }

        #region Routes

        private RouteCollection _routes;

        [TestMethod]
        public void IgnoredRoute_ResourceAxd()
        {
            AssertIgnoredRoute("~/some.axd/path");
        }

        [TestMethod]
        public void IgnoredRoute_Home()
        {
            AssertTreatedAsResourceNotFound("~/home");
        }

        [TestMethod]
        public void IgnoredRoute_Home_ParametersExplicit()
        {
            AssertTreatedAsResourceNotFound("~/home?id=55");
        }

        /// <summary>
        /// This Test fails because we don't explicitly ignore route /Home/{id}.
        /// So it is handled in default route: "{controller}/{action}/{id}".
        /// </summary>
        // [TestMethod]
        public void IgnoredRoute_Home_ParametersImplicit()
        {
            AssertTreatedAsResourceNotFound("~/home/55");
        }

        [TestMethod]
        public void NotIgnoredRoute_HomeSetup()
        {
            AssertNotTreatedAsResourceNotFound("~/home/setup");
        }

        [TestMethod]
        public void IgnoredRoute_HomeIndex()
        {
            AssertTreatedAsResourceNotFound("~/home/index");
        }

        [TestMethod]
        public void IgnoredRoute_HomeIndex_ParametersExplicit()
        {
            AssertTreatedAsResourceNotFound("~/home/index?id=55");
        }

        [TestMethod]
        public void IgnoredRoute_HomeIndex_ParametersImplicit()
        {
            AssertTreatedAsResourceNotFound("~/home/index/55");
        }

        [TestMethod]
        public void IgnoredRoute_UserIndex()
        {
            AssertTreatedAsResourceNotFound("~/user/index");
        }

        [TestMethod]
        public void NotIgnoredRoute_UserSettings()
        {
            AssertNotTreatedAsResourceNotFound("~/user/settings");
        }

        public void AssertTreatedAsResourceNotFound(string relativeUri)
        {
            // Act
            HttpContextBase context = MvcMockHelpers.FakeHttpContext(relativeUri);
            RouteData routeData = _routes.GetRouteData(context);

            // Assert
            Assert.AreEqual("Error", routeData.Values["controller"]);
            Assert.AreEqual("NotFound", routeData.Values["action"]);
        }

        public void AssertNotTreatedAsResourceNotFound(string relativeUri)
        {
            // Act
            HttpContextBase context = MvcMockHelpers.FakeHttpContext(relativeUri);
            RouteData routeData = _routes.GetRouteData(context);

            // Assert
            Assert.AreNotEqual("NotFound", routeData.Values["action"]);
        }

        public void AssertIgnoredRoute(string relativeUri)
        {
            // Act
            HttpContextBase context = MvcMockHelpers.FakeHttpContext(relativeUri);
            RouteData routeData = _routes.GetRouteData(context);

            // Assert
            Assert.IsInstanceOfType(routeData.RouteHandler, typeof (StopRoutingHandler));
        }

        public void AssertNotIgnoredRoute(string relativeUri)
        {
            // Act
            HttpContextBase context = MvcMockHelpers.FakeHttpContext(relativeUri);
            RouteData routeData = _routes.GetRouteData(context);

            // Assert
            Assert.IsNotInstanceOfType(routeData.RouteHandler, typeof (StopRoutingHandler));
        }

        #endregion

        [TestMethod]
        public void WwwSubdomainRegex_MustMatchHttpWwwAddress()
        {
            // Act
            bool result = WwwSubdomainRegex_MustMatch("http://www.absolute.com.ar/test/path");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void WwwSubdomainRegex_MustMatchHttpsWwwAddress()
        {
            // Act
            bool result = WwwSubdomainRegex_MustMatch("https://www.absolute.com.ar/test/path");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void WwwSubdomainRegex_DoesNotMatchHttpAddress()
        {
            // Act
            bool result = WwwSubdomainRegex_MustMatch("http://absolute.com.ar/test/path");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WwwSubdomainRegex_DoesNotMatchHttpsAddress()
        {
            // Act
            bool result = WwwSubdomainRegex_MustMatch("https://absolute.com.ar/test/path");

            // Assert
            Assert.IsFalse(result);
        }

        public bool WwwSubdomainRegex_MustMatch(string test)
        {
            // Arrange
            Regex regex = CompiledRegex.WwwSubdomain;
            return regex.IsMatch(test);
        }
    }
}
