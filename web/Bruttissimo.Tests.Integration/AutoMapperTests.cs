using Bruttissimo.Common;
using Bruttissimo.Domain.Entity;
using Bruttissimo.Mvc.Model;
using Bruttissimo.Tests.Mocking;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bruttissimo.Tests.Integration
{
    [TestClass]
    public class AutoMapperTests
    {
        private IWindsorContainer container;
        private IMapper mapper;

        [TestInitialize]
        public void TestInit()
        {
            container = IntegrationMockHelpers.GetWindsorContainer();
            mapper = container.Resolve<IMapper>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            container.Dispose();
        }

        [TestMethod]
        public void AutoMapperShouldBeProperlyConfigured()
        {
            mapper.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void PostShouldMapToConcreteLinkPostModel()
        {
            Post post = new Post { Link = new Link { Type = LinkType.Html } };

            PostModel model = mapper.Map<Post, PostModel>(post);

            Assert.IsInstanceOfType(model, typeof(LinkPostModel));
        }

        [TestMethod]
        public void PostShouldMapPropertiesToConcreteLinkPostModel()
        {
            Post post = new Post { Link = new Link { Type = LinkType.Html, Description = "desc" } };

            PostModel model = mapper.Map<Post, PostModel>(post);

            Assert.AreEqual("desc", ((LinkPostModel)model).LinkDescription);
        }

        [TestMethod]
        public void PostShouldMapToConcreteImagePostModel()
        {
            Post post = new Post { Link = new Link { Type = LinkType.Image } };

            PostModel model = mapper.Map<Post, PostModel>(post);

            Assert.IsInstanceOfType(model, typeof(ImagePostModel));
        }

        [TestMethod]
        public void PostShouldMapPropertiesToConcreteImagePostModel()
        {
            Post post = new Post { Link = new Link { Type = LinkType.Image, Picture = "pic" } };

            PostModel model = mapper.Map<Post, PostModel>(post);

            Assert.AreEqual("pic", ((ImagePostModel)model).LinkPicture);
        }
    }
}
