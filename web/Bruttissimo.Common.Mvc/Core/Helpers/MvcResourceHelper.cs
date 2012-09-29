using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Bruttissimo.Common.Guard;

namespace Bruttissimo.Common.Mvc
{
    public class MvcResourceHelper : ResourceHelper<IHtmlString>, IMvcResourceHelper
    {
        private readonly HtmlHelper helper;
        private readonly Assembly assembly;

        public MvcResourceHelper(string namespaceRoot, HtmlHelper helper, Assembly assembly)
            : base(namespaceRoot)
        {
            Ensure.That(helper, "helper").IsNotNull();
            Ensure.That(assembly, "assembly").IsNotNull();

            this.helper = helper;
            this.assembly = assembly;
        }

        #region Overrides of ResourceHelper

        protected override Assembly GetReferenceAssembly()
        {
            return assembly;
        }

        protected override string GetViewPath()
        {
            return ((WebViewPage)helper.ViewDataContainer).VirtualPath;
        }

        protected override string GetSharedResourceNamespace()
        {
            return Resources.Constants.MvcResourceSharedNamespace;
        }

        protected override IHtmlString RawConverter(string resource)
        {
            return new HtmlString(resource);
        }

        #endregion
    }
}
