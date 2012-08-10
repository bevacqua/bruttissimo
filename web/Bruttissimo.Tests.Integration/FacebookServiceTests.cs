using Bruttissimo.Common;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Logic;
using Bruttissimo.Domain.Social;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bruttissimo.Tests.Integration
{
	[TestClass]
	public class FacebookServiceTests
	{
		private IFacebookService facebookService;
	    private IFacebookRepository facebookRepository;

		[TestInitialize]
		public void TestInit()
		{
			string token = Config.Social.FacebookAccessToken;
            facebookRepository = new FacebookRepository(token);
            facebookService = new FacebookService(facebookRepository);
		}

		[TestMethod]
		public void FacebookProvider_CanGetPostsInGroupFeed()
		{
			// Arrange
			string group = Config.Social.FacebookGroupId;
			string next;

			// Act
			// facebookService.Import();
		    facebookRepository.GetPostsInGroupFeed(group, out next);
		}
	}
}
