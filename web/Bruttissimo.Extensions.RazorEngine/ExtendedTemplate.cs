using Bruttissimo.Common;
using RazorEngine.Templating;

namespace Bruttissimo.Extensions.RazorEngine
{
    /// <summary>
    /// Extended templating base.
    /// </summary>
    public class ExtendedTemplate<T> : TemplateBase<T>, IExtendedTemplate
    {
        private TemplateResourceHelper resource;
        private TemplateUrlHelper url;

        public TemplateResourceHelper Resource
        {
            get { return resource; }
            set { resource = resource.InjectProperty(value, "Resource"); }
        }

        public TemplateUrlHelper Url
        {
            get { return url; }
            set { url = url.InjectProperty(value, "Url"); }
        }

        public override TemplateWriter Include(string cacheName, object model)
        {
            var instance = TemplateService.Resolve(cacheName, model);
            var instanceHas = instance as IExtendedTemplate;
            if (instance == null || instanceHas == null)
            {
                return base.Include(cacheName, model);
            }
            return new TemplateWriter(tw => // tweak resource helper's context.
            {
                instanceHas.Resource.ManualCacheKeyOverride = cacheName;
                tw.Write(instance.Run(new ExecuteContext(cacheName)));
                instanceHas.Resource.ManualCacheKeyOverride = null;
            });
        }
    }
}
