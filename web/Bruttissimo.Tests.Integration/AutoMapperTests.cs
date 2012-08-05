using AutoMapper;
using Bruttissimo.Tests.Mocking;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bruttissimo.Tests.Integration
{
	[TestClass]
	public class AutoMapperTests
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

		[TestMethod]
		public void AutoMapperShouldBeProperlyConfigured()
		{
			Mapper.AssertConfigurationIsValid();
		}
	}
}
