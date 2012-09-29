using System;

namespace Bruttissimo.Domain.Entity.Entities
{
    public class Comment
    {
        public long Id { get; set; }

        public long UserId { get; set; }
        public long PostId { get; set; }
        public long? ParentId { get; set; }

        public string Message { get; set; }

        public DateTime Created { get; set; }
    }
}
