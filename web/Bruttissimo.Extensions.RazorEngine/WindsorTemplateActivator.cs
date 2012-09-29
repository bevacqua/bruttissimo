using Bruttissimo.Common.Guard;
using Bruttissimo.Extensions.RazorEngine.Interface;
using Castle.MicroKernel;
using RazorEngine.Templating;

namespace Bruttissimo.Extensions.RazorEngine
{
    public class WindsorTemplateActivator : IActivator
    {
        private readonly IKernel kernel;

        public WindsorTemplateActivator(IKernel kernel)
        {
            Ensure.That(kernel, "kernel").IsNotNull();

            this.kernel = kernel;
        }

        public ITemplate CreateInstance(InstanceContext context)
        {
            Ensure.That(context, "context").IsNotNull();

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
