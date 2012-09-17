using Bruttissimo.Common;
using Bruttissimo.Tests.Mocking;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mapper = AutoMapper.Mapper;

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
    }
}
