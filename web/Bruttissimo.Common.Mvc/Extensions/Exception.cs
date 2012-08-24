using System;
using System.Net;
using System.Web;

namespace Bruttissimo.Common.Mvc
{
    public static class ExceptionExtensions
    {
        public static bool IsHttpNotFound(this Exception exception)
        {
            if (exception is HttpException)
            {
                return ((HttpException)exception).GetHttpCode() == (int)HttpStatusCode.NotFound;
            }
            return false;
        }
    }
}
