using System.Collections;
using System.Collections.Generic;

namespace Bruttissimo.Mvc.Models
{
	public class ResourceLocalizationModel
	{
		public string Title { get; set; }
		public IEnumerable<DictionaryEntry> Items { get; set; }
	}
}