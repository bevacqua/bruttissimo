using System;

namespace Bruttissimo.Domain
{
	public class Post
	{
		public long Id { get; set; }

		public long? UserId { get; set; }
		public User User { get; set; }

		public long? LinkId { get; set; }
		public Link Link { get; set; }

		public string FacebookGroupId { get; set; }
		public string FacebookPostId { get; set; }
		public string FacebookUserId { get; set; }

		public string UserMessage { get; set; }

		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
	}
}
