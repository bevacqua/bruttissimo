using System.Collections.Generic;

namespace Bruttissimo.Domain
{
	public class UserRole
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public IEnumerable<UserRight> Rights { get; set; }
	}
}