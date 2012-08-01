using System;
using System.Collections.Generic;

namespace Bruttissimo.Domain
{
	public interface ILinkService
	{
		LinkResult ParseUserInput(string text);
		IList<Uri> GetReferenceUris(string text);
	}
}