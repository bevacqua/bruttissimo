using System;
using Castle.MicroKernel;
using RazorEngine.Templating;

namespace Bruttissimo.Extensions.RazorEngine
{
    public class WindsorTemplateActivator : IActivator
    {
        private readonly IKernel kernel;

        public WindsorTemplateActivator(IKernel kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException("kernel");
            }
            this.kernel = kernel;
        }

        public ITemplate CreateInstance(InstanceContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            ITemplate template = context.Loader.CreateInstance(context.TemplateType);
            IExtendedTemplate service = template as IExtendedTemplate;
            if (service != null)
            {
                service.Resource = kernel.Resolve<ITemplateResourceHelper>(new { templateBase = template });
                service.Url = kernel.Resolve<TemplateUrlHelper>();
            }
            return template;
        }
    }
}
