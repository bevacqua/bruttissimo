using System;
using System.Collections.Generic;
using Bruttissimo.Common;
using Facebook;

namespace Bruttissimo.Domain.Social
{
	public class FacebookService
	{
		private const int DEFAULT_PAGE_LIMIT = 15;
		private const string GRAPH_FEED_LIMITED = "{0}/feed?limit={1}";

		private readonly string accessToken;

		public FacebookService(string accessToken)
		{
			if (accessToken == null)
			{
				throw new ArgumentNullException("accessToken");
			}
			this.accessToken = accessToken;
		}

		public IList<FacebookPost> GetPostsInGroupFeed(string group, out bool more)
		{
			FacebookClient client = new FacebookClient(accessToken);
			string feed = GRAPH_FEED_LIMITED.FormatWith(group, DEFAULT_PAGE_LIMIT);
			dynamic response = client.Get(feed);
			throw new NotImplementedException(GRAPH_FEED_LIMITED);
		}


		private dynamic a(dynamic response)
		{
			if (response.error_code == null && response.error_msg == null)
			{
				return response;
			}
			else
			{
				return new FacebookApiException(response.error_msg)
				{
					ErrorType = response.error_code
				};
			}
		}
	}
}
