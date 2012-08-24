using System.Collections.Generic;

namespace Bruttissimo.Domain.Entity
{
    public class Role
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Right> Rights { get; set; }
    }
}
