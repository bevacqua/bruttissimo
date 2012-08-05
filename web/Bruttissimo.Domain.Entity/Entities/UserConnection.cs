
namespace Bruttissimo.Domain.Entity
{
	public class UserConnection
	{
		public long Id { get; set; }
		public string OpenId { get; set; }
		public string TwitterId { get; set; }
		public string FacebookId { get; set; }
		public string FacebookAccessToken { get; set; }

		public long UserId { get; set; }
	}
}
