using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain;
using Bruttissimo.Tests.Mocking;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bruttissimo.Tests.Integration
{
    [TestClass]
    public class TwitterRepositoryTests
    {
        private IWindsorContainer container;

        [TestInitialize]
        public void TestInit()
        {
            container = IntegrationMockHelpers.GetWindsorContainer();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            container.Dispose();
        }

        // [TestMethod]
        public void PostTweetToFeed()
        {
            ITwitterRepository twitterRepository = container.Resolve<ITwitterRepository>();
            twitterRepository.PostToFeed("Mujer muere baleada con balas https://github.com/danielcrenna/tweetsharp");
        }
    }
}