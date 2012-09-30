using Bruttissimo.Common.Interface;
using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Domain.Entity.Enum;
using Bruttissimo.Mvc.Model.ViewModels;
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
        public void AutoMapper_should_be_properly_configured()
        {
            mapper.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void Post_should_map_to_concrete_LinkPostModel()
        {
            Post post = new Post { Link = new Link { Type = LinkType.Html } };

            PostModel model = mapper.Map<Post, PostModel>(post);

            Assert.IsInstanceOfType(model, typeof(LinkPostModel));
        }

        [TestMethod]
        public void Post_should_map_properties_to_concrete_LinkPostModel()
        {
            Post post = new Post { Link = new Link { Type = LinkType.Html, Description = "desc" } };

            PostModel model = mapper.Map<Post, PostModel>(post);

            Assert.AreEqual("desc", ((LinkPostModel)model).LinkDescription);
        }

        [TestMethod]
        public void Post_should_map_to_concrete_ImagePostModel()
        {
            Post post = new Post { Link = new Link { Type = LinkType.Image } };

            PostModel model = mapper.Map<Post, PostModel>(post);

            Assert.IsInstanceOfType(model, typeof(ImagePostModel));
        }

        [TestMethod]
        public void post_should_map_properties_to_concrete_ImagePostModel()
        {
            Post post = new Post { Link = new Link { Type = LinkType.Image, Picture = "pic" } };

            PostModel model = mapper.Map<Post, PostModel>(post);

            Assert.AreEqual("pic", ((ImagePostModel)model).LinkPicture);
        }

        [TestMethod]
        public void Post_should_map_CommentListModel_even_if_empty()
        {
            Post post = new Post { Id = 2, Link = new Link { Type = LinkType.Image, Picture = "pic" } };

            CommentListModel model = mapper.Map<Post, CommentListModel>(post);

            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void Post_should_map_Comments_property_even_if_empty()
        {
            Post post = new Post { Id = 2, Link = new Link { Type = LinkType.Image, Picture = "pic" } };

            PostModel model = mapper.Map<Post, PostModel>(post);

            Assert.IsNotNull(model.Comments);
        }
    }
}
