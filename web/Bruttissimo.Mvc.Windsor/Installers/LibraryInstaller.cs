using Bruttissimo.Domain;
using Bruttissimo.Domain.Logic;
using Bruttissimo.Domain.Logic.Email.Template;
using Bruttissimo.Extensions.RazorEngine;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DotNetOpenAuth.OpenId.RelyingParty;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace Bruttissimo.Mvc.Windsor
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
                    .For<TemplateResourceHelper>()
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

            container.Register(
                Component
                    .For(typeof (IHubContextWrapper<>))
                    .ImplementedBy(typeof (HubContextWrapper<>))
                    .LifestyleTransient()
                );
        }

        internal IEmailTemplateService InstanceEmailTemplateService(IKernel kernel)
        {
            TemplateServiceConfiguration configuration = new TemplateServiceConfiguration
            {
                Activator = new WindsorTemplateActivator(kernel),
                BaseTemplateType = typeof (ExtendedTemplate<>),
                Resolver = new EmbeddedTemplateResolver(typeof (EmailTemplate))
            };
            IEmailTemplateService service = new EmailTemplateService(configuration);
            return service;
        }

        internal TemplateResourceHelper InstanceTemplateResourceHelper(IKernel kernel, ComponentModel model, CreationContext context)
        {
            TemplateBase templateBase = context.AdditionalArguments["templateBase"] as TemplateBase;
            return new TemplateResourceHelper(Common.Resources.Email.ResourceNamespaceRoot, templateBase);
        }
    }
}
