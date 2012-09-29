using System;
using Bruttissimo.Domain.Entity.Entities;

namespace Bruttissimo.Domain.DTO.Facebook
{
    public class FacebookImportOptions
    {
        public string Feed { get; set; }
        public DateTime? Since { get; set; }
        public FacebookImportLog Log { get; set; }
    }
}
