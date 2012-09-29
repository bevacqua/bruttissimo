using System;
using System.Net;
using System.Web;

namespace Bruttissimo.Common.Mvc.Extensions
{
    public static class ExceptionExtensions
    {
        public static bool IsHttpNotFound(this Exception exception)
        {
            var httpException = exception as HttpException;
            if (httpException != null)
            {
                return httpException.GetHttpCode() == (int)HttpStatusCode.NotFound;
            }
            return false;
        }
    }
}
