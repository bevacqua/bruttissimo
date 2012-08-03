namespace Bruttissimo.Domain.Social
{
	public class FacebookUserProvider : FacebookProvider
	{
		public FacebookUserProvider(string accessToken)
			: base(accessToken)
		{
		}
	}
}