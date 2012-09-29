using System.Collections.Generic;

namespace Bruttissimo.Domain.Entity.Entities
{
    public class Role
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Right> Rights { get; set; }
    }
}
