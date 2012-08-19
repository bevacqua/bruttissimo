using System.Collections;
using System.Collections.Generic;

namespace Bruttissimo.Common.Mvc
{
    public class ResourceLocalizationModel
    {
        public string Title { get; set; }
        public IEnumerable<DictionaryEntry> Items { get; set; }
    }
}
