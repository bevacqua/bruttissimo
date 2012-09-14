using System;
using System.Web.Mvc;

namespace Bruttissimo.Common.Mvc
{
    public interface IJavaScriptHelper
    {
        void Register(string path, string source, Guid? guid = null);
        MvcHtmlString Emit(Guid? guid = null);
    }
}