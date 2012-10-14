using System;
using System.Collections.Generic;

namespace Bruttissimo.Domain.Entity.Entities
{
    public class Smiley
    {
        public long Id { get; set; }
        public string ResourceKey { get; set; }
        public string Emoticon { get; set; }
        public string Aliases { get; set; }
        public string CssClass { get; set; }
    }
}