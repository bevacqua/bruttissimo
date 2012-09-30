using Bruttissimo.Common.Resources;
using Bruttissimo.Domain.Logic.Email.RazorEngine;
using Bruttissimo.Domain.Logic.Email.Template;
using Bruttissimo.Extensions.RazorEngine;
using Bruttissimo.Extensions.RazorEngine.Interface;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DotNetOpenAuth.OpenId.RelyingParty;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace Bruttissimo.Mvc.Windsor.Installers
{
    /// <summary>
    /// Registers all external dependencies, such as RazorEngine templating services.
    /// </summary>
    public class LibraryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // RazorEngine templating service implementation for email templates.
            container.Register(
                Component
                    .For<IEmailTemplateService>()
                    .UsingFactoryMethod(InstanceEmailTemplateService)
                    .LifestyleSingleton()
                );

            container.Register(
                Component
                    .For<ITemplateResourceHelper>()
                    .UsingFactoryMethod(InstanceTemplateResourceHelper)
                    .LifestyleTransient()
                );

            container.Register(
                Component
                    .For<TemplateUrlHelper>()
                    .ImplementedBy<TemplateUrlHelper>()
                    .LifestyleTransient()
                );

            // DotNetOpenAuth's back-end implementation for OpenId protocol communication.
            container.Register(
                Component
                    .For<OpenIdRelyingParty>()
                    .ImplementedBy<OpenIdRelyingParty>()
                    .LifestyleSingleton()
                );
        }

        internal IEmailTemplateService InstanceEmailTemplateService(IKernel kernel)
        {
            TemplateServiceConfiguration configuration = new TemplateServiceConfiguration
            {
                Activator = new WindsorTemplateActivator(kernel),
                BaseTemplateType = typeof(ExtendedTemplate<>),
                Resolver = new EmbeddedTemplateResolver(typeof(EmailTemplate))
            };
            IEmailTemplateService service = new EmailTemplateService(configuration);
            return service;
        }

        internal ITemplateResourceHelper InstanceTemplateResourceHelper(IKernel kernel, ComponentModel model, CreationContext context)
        {
            TemplateBase templateBase = context.AdditionalArguments["templateBase"] as TemplateBase;
            string @namespace = typeof(EmailTemplate).Namespace;
            return new TemplateResourceHelper(@namespace, templateBase);
        }
    }
}
