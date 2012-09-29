using System.Web.Mvc;

namespace Bruttissimo.Common.Mvc.Core.ActionResults.Json
{
    public class JsonRedirectResult : JsonResult
    {
        public JsonRedirectResult(RedirectResult redirectResult)
        {
            Data = new
            {
                redirect = true,
                href = redirectResult.Url
            };
        }
    }
}
