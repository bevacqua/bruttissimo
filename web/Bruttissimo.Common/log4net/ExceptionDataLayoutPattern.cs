using System.Collections;
using System.IO;
using log4net.Core;
using log4net.Layout.Pattern;

namespace log4net.Layout
{
	public sealed class ExceptionDataPattern : PatternLayoutConverter
	{
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			if (loggingEvent.ExceptionObject == null)
			{
				return;
			}
			IDictionary data = loggingEvent.ExceptionObject.Data;
			if (data.Contains(Option))
			{
				writer.Write(data[Option]);
			}
		}
	}
}