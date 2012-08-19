using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;

namespace Bruttissimo.Tests.Mocking
{
    public static class MvcMockHelpers
    {
        public static HttpContextBase FakeHttpContext()
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var requestContext = new Mock<RequestContext>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();
            var user = new Mock<IPrincipal>();
            var identity = new Mock<IIdentity>();

            request.Setup(x => x.ApplicationPath).Returns("~/");
            request.Setup(x => x.AppRelativeCurrentExecutionFilePath).Returns("~/");
            request.Setup(x => x.PathInfo).Returns(string.Empty);
            request.Setup(x => x.RequestContext).Returns(requestContext.Object);
            response.Setup(x => x.ApplyAppPathModifier(It.IsAny<string>())).Returns((string virtualPath) => virtualPath);
            user.Setup(x => x.Identity).Returns(identity.Object);
            identity.SetupGet(x => x.IsAuthenticated).Returns(true);
            server.Setup(x => x.MapPath(It.IsAny<string>())).Returns<string>(path => @"c:" + path.Substring(1, path.Length - 1));

            context.Setup(x => x.Request).Returns(request.Object);
            context.Setup(x => x.Response).Returns(response.Object);
            context.Setup(x => x.Session).Returns(session.Object);
            context.Setup(x => x.Server).Returns(server.Object);
            context.Setup(x => x.User).Returns(user.Object);

            return context.Object;
        }

        public static HttpContextBase FakeHttpContext(string url)
        {
            HttpContextBase context = FakeHttpContext();
            context.Request.SetupRequestUrl(url);
            return context;
        }

        public static void SetFakeControllerContext(this Controller controller)
        {
            var httpContext = FakeHttpContext();
            ControllerContext context = new ControllerContext(new RequestContext(httpContext, new RouteData()), controller);
            controller.ControllerContext = context;
        }

        public static string GetUrlFileName(string url)
        {
            if (url.Contains("?"))
            {
                return url.Substring(0, url.IndexOf("?"));
            }
            else
            {
                return url;
            }
        }

        public static void SetHttpMethodResult(this HttpRequestBase request, string httpMethod)
        {
            Mock.Get(request)
                .Setup(req => req.HttpMethod)
                .Returns(httpMethod);
        }

        public static void SetupRequestUrl(this HttpRequestBase request, string url)
        {
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }
            if (!url.StartsWith("~/"))
            {
                throw new ArgumentException(@"Sorry, we Setup a virtual url starting with ""~/"".");
            }

            Mock<HttpRequestBase> mock = Mock.Get(request);

            mock.Setup(x => x.QueryString)
                .Returns(HttpUtility.ParseQueryString(url));
            mock.Setup(x => x.AppRelativeCurrentExecutionFilePath)
                .Returns(GetUrlFileName(url));
            mock.Setup(x => x.PathInfo)
                .Returns(string.Empty);
        }
    }
}
