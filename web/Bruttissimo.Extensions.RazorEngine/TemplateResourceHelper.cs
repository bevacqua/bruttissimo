using System;
using System.Reflection;
using Bruttissimo.Common;
using Bruttissimo.Common.Guard;
using RazorEngine.Templating;
using RazorEngine.Text;

namespace Bruttissimo.Extensions.RazorEngine
{
    public class TemplateResourceHelper : ResourceHelper<IEncodedString>, ITemplateResourceHelper
    {
        /// <summary>
        /// This allows manual tweaks regarding where a resource is supposed to be.
        /// </summary>
        public string ManualCacheKeyOverride { protected get; set; }

        private readonly TemplateBase templateBase;

        public TemplateResourceHelper(string resourceNamespaceRoot, TemplateBase templateBase)
            : base(resourceNamespaceRoot)
        {
            Ensure.That(templateBase, "templateBase").IsNotNull();

            this.templateBase = templateBase;
        }

        #region Overrides of ResourceHelper

        protected override Assembly GetReferenceAssembly()
        {
            EmbeddedTemplateResolver resolver = templateBase.TemplateService.Resolver as EmbeddedTemplateResolver;
            if (resolver == null)
            {
                throw new InvalidOperationException(Common.Resources.RazorEngine.EmbeddedTemplateResolverNotFound);
            }
            Assembly assembly = resolver.ResourceAssembly;
            return assembly;
        }

        protected override string GetViewPath()
        {
            string currentContextCacheKey = ManualCacheKeyOverride ?? templateBase.CurrentContext.CacheKey; // this is in place to allow manual tweaks regarding where a resource is supposed to be.
            string cacheKey = currentContextCacheKey.Replace('.', '/'); // adapt to the pattern expected by ResourceHelper base.
            string viewPath = Common.Resources.RazorEngine.TemplateName.FormatWith(cacheKey); // append view extension.
            return viewPath;
        }

        protected override string GetSharedResourceNamespace()
        {
            return Common.Resources.RazorEngine.ResourceSharedNamespace;
        }

        protected override IEncodedString RawConverter(string resource)
        {
            return new RawString(resource);
        }

        #endregion
    }
}
