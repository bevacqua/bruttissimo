﻿using System.Collections;
using System.Collections.Generic;

namespace Bruttissimo.Common.Mvc.Core.Models
{
    public class ResourceLocalizationModel
    {
        public string Title { get; set; }
        public IEnumerable<DictionaryEntry> Items { get; set; }
    }
}
