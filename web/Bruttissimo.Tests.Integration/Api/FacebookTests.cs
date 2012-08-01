using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Bruttissimo.Common;
using Bruttissimo.Common.Resources;
using Bruttissimo.Domain.Social.Facebook;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bruttissimo.Tests.Integration
{
	[TestClass]
	public class FacebookTests
	{
		private FacebookGroupProvider groupProvider;

		[TestInitialize]
		public void TestInit()
		{
			string token = Config.Social.FacebookAccessToken;
			groupProvider = new FacebookGroupProvider(token);
		}

		[TestMethod]
		public void FacebookProvider_CanPostToGroupWall()
		{
			// Arrange
			string groupId = Config.Social.FacebookGroupId;
			GroupPostParams parameters = new GroupPostParams
			{
				GroupId = groupId,
				UserMessage = "FacebookProvider_CanPostToGroupWall test link post, then linebreak." + Constants.NewLine + "New line: Link follows >",
				Link = "http://google.com/"
			};

			// Act
			dynamic response = groupProvider.PostToGroup(parameters);

			// Assert
			Assert.IsFalse(response is Exception);
		}

		[TestMethod]
		public void FacebookProvider_CanGetGroupFeedPosts()
		{
			// Arrange
			string groupId = Config.Social.FacebookGroupId;
			GroupGetParams parameters = new GroupGetParams
			{
				GroupId = groupId
			};

			// Act
			dynamic response = groupProvider.GetRecentGroupFeedPosts(parameters);

			// Assert
			Assert.IsFalse(response is Exception);
		}

		// [TestMethod] // very long running test method
		public void FacebookProvider_CanGetEverySingleGroupFeedPost()
		{
			// Arrange
			string groupId = "181282708550211"; // actual group
			GroupGetParams parameters = new GroupGetParams
			{
				GroupId = groupId,
				Limit = 200
			};

			// Act
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			IList<dynamic> response = groupProvider.GetAllPostsInGroupFeed(parameters);
			stopwatch.Stop();

			// Assert
			Assert.IsFalse(response.Any(result => result is Exception));
		}
	}
}
