using System.Collections.Generic;

namespace Bruttissimo.Domain.Entity
{
	public class UserRole
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public IEnumerable<UserRight> Rights { get; set; }
	}
}