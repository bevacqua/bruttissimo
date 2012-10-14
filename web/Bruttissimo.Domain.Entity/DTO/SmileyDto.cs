using System;
using System.Collections.Generic;

namespace Bruttissimo.Domain.Entity.DTO
{
    public class SmileyDto
    {
        public IEnumerable<string> Keywords { get; set; }
        public IEnumerable<string> EncodedKeywords { get; set; }
        public Lazy<string> Smiley { get; set; }
    }
}