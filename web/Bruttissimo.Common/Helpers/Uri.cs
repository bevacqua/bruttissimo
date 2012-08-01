using System;

namespace Bruttissimo.Common
{
	public static class UriHelpers
	{
		public static bool IsAbsoluteUri(this string uriText)
		{
			Uri uri;
			bool result = Uri.TryCreate(uriText, UriKind.Absolute, out uri);
			return result;
		}

		public static Uri ToUri(this string uriText, Uri baseUri)
		{
			if (uriText.IsAbsoluteUri())
			{
				return new Uri(uriText);
			}
			return new Uri(baseUri, uriText);
		}

		public static Uri ToUriLocal(this string uriText, Uri baseUri)
		{
			Uri uri = uriText.ToUri(baseUri);
			if (uri.Host != baseUri.Host)
			{
				return baseUri;
			}
			return uri;
		}
	}
}