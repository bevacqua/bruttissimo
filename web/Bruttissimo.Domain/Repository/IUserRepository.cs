namespace Bruttissimo.Domain
{
	public interface IUserRepository
	{
		bool AreMatchingPasswords(string password, string databaseHash);
		bool IsInRoleOrHasRight(User user, string roleOrRight);
		User GetByEmail(string email);
		User GetByUserId(long id);
		User GetByOpenId(string openId);
		User GetByFacebookGraphId(string facebookId);
		User GetByTwitterId(string twitterId);
		User CreateWithCredentials(string email, string password);
		User CreateWithOpenId(string openId, string email, string displayName);
		User CreateWithFacebook(string facebookId, string accessToken, string email, string displayName);
		User CreateWithTwitter(string twitterId, string displayName);
		UserConnection AddFacebookConnection(User user, string facebookId, string accessToken);
		UserConnection AddOpenIdConnection(User user, string openId);
	}
}