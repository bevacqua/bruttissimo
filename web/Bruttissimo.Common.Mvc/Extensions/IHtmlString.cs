using System.Web;
using Bruttissimo.Common.Extensions;

namespace Bruttissimo.Common.Mvc.Extensions
{
    public static class IHtmlStringExtensions
    {
        public static bool NullOrEmpty(this IHtmlString html)
        {
            string htmlString = html.ToHtmlString();
            bool result = htmlString.NullOrEmpty();
            return result;
        }
    }
}
