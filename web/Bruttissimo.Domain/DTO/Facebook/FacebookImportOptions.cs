using System;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
    public class FacebookImportOptions
    {
        public string Feed { get; set; }
        public DateTime? Since { get; set; }
        public FacebookImportLog Log { get; set; }
    }
}
