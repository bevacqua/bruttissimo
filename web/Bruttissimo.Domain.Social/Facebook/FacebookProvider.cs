using System;
using Facebook;

namespace Bruttissimo.Domain.Social.Facebook
{
	public abstract class FacebookProvider
	{
		internal protected readonly string accessToken;

		internal protected FacebookProvider(string accessToken)
		{
			if (accessToken == null)
			{
				throw new ArgumentNullException("accessToken");
			}
			this.accessToken = accessToken;
		}

		internal protected dynamic ParseResponseExceptions(dynamic response)
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
