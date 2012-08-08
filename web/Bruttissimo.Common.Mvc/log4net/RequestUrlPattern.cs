using System.IO;
using System.Web;
using log4net.Core;
using log4net.Layout.Pattern;

namespace log4net.Layout
{
	public sealed class RequestUrlPattern : PatternLayoutConverter
	{
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			if (HttpContext.Current == null)
			{
				return;
			}
			try
			{
				writer.Write(HttpContext.Current.Request.RawUrl);
			}
			catch (HttpException) // when attempting to access the request in an HttpContext that isn't part of a Request.
			{
				// do nothing.
			}
		}
	}
}