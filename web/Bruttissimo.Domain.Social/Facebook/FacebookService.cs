using System;
using System.Collections.Generic;
using Facebook;

namespace Bruttissimo.Domain.Social
{
	public class FacebookService
	{
		private readonly string accessToken;

		public FacebookService(string accessToken)
		{
			if (accessToken == null)
			{
				throw new ArgumentNullException("accessToken");
			}
			this.accessToken = accessToken;
		}

		public IList<FacebookPost> GetPostsInGroupFeed(long group, out bool more)
		{
			throw new NotImplementedException();
		}

		private const string TEMP_NOMBRE_ENDPOINT = "{0}/feed?limit={1}";

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
