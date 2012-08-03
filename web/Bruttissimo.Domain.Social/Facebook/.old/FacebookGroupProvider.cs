using System;
using System.Collections.Generic;
using Bruttissimo.Common;
using Bruttissimo.Common.Resources;
using Facebook;

namespace Bruttissimo.Domain.Social
{
	public class FacebookGroupProvider
	{
		private readonly string accessToken;

		public FacebookGroupProvider(string accessToken)
		{
			if (accessToken == null)
			{
				throw new ArgumentNullException("accessToken");
			}
			this.accessToken = accessToken;
		}

		public dynamic PostToGroup(GroupPostParams parameters)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			if (parameters.GroupId.NullOrEmpty())
			{
				throw new ArgumentNullException("parameters.GroupId");
			}
			string feed = FacebookApi.GroupFeed.FormatWith(parameters.GroupId);
			FacebookClient client = new FacebookClient(accessToken);
			try
			{
				dynamic response = client.Post(feed, new
				{
					message = parameters.UserMessage,
					link = parameters.Link,
					picture = parameters.Picture,
					name = parameters.Name,
					caption = parameters.Caption,
					description = parameters.Description
				});
				dynamic result = ParseResponseExceptions(response);
				return result;
			}
			catch (FacebookApiException exception) // we just return the exception instead of throwing it.
			{
				return exception;
			}
		}

		public dynamic GetRecentGroupFeedPosts(GroupGetParams parameters)
		{
			string feed = FacebookApi.GroupFeed.FormatWith(parameters.GroupId);
			FacebookClient client = new FacebookClient(accessToken);
			try
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>
				{
				    {"limit", parameters.Limit}
				};
				dynamic response = client.Get(feed, dictionary);
				dynamic result = ParseResponseExceptions(response);
				return result;
			}
			catch (FacebookApiException exception) // we just return the exception instead of throwing it.
			{
				return exception;
			}
		}

		public dynamic GetAllPostsInGroupFeed(GroupGetParams parameters)
		{
			ICollection<dynamic> results = new List<dynamic>();
			string feed = FacebookApi.GroupFeed.FormatWith(parameters.GroupId);
			FacebookClient client = new FacebookClient(accessToken);
			try
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>
				{
				    {"limit", parameters.Limit}
				};
				bool complete = false;
				do
				{
					dynamic response = client.Get(feed, dictionary);
					dynamic result = ParseResponseExceptions(response);
					results.Add(result);
					if (result is Exception || result.paging == null || result.paging.next == feed)
					{
						complete = true;
					}
					else
					{
						feed = result.paging.next;
					}
				} while (!complete);
			}
			catch (FacebookApiException exception) // we just return the exception instead of throwing it.
			{
				results.Add(exception);
			}
			return results;
		}

		internal dynamic ParseResponseExceptions(dynamic response)
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
