using System.IO;
using System.Net;

namespace Bruttissimo.Common
{
    public static class WebResponseExtensions
    {
        public static string GetResponseString(this WebResponse response)
        {
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
