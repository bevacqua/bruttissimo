using System;

namespace Bruttissimo.Domain.Entity
{
    public class Link
    {
        public long Id { get; set; }

        public LinkType Type { get; set; }

        public string ReferenceUri { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }

        public DateTime Created { get; set; }

        public long? PostId;
    }
}
