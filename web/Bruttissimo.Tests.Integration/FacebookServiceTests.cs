using System;
using System.Collections.Generic;
using Bruttissimo.Common;
using Bruttissimo.Domain.Social;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bruttissimo.Tests.Integration
{
	[TestClass]
	public class FacebookServiceTests
	{
		private FacebookService facebookService;

		[TestInitialize]
		public void TestInit()
		{
			string token = Config.Social.FacebookAccessToken;
			facebookService = new FacebookService(token);
		}

		[TestMethod]
		public void FacebookProvider_CanPostToGroupWall()
		{
			// Arrange
			string group = Config.Social.FacebookGroupId;
			bool more;

			// Act
			IList<FacebookPost> response = facebookService.GetPostsInGroupFeed(group, out more);

			// Assert
			Assert.IsFalse(response is Exception);
		}
	}
}
