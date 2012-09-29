using System;
using System.Net.Mail;
using Bruttissimo.Common;
using Bruttissimo.Common.Guard;
using Bruttissimo.Domain.Entity;
using Bruttissimo.Domain.Logic.Email.Model;
using RazorEngine.Templating;
using log4net;

namespace Bruttissimo.Domain.Logic
{
    public class EmailService : IEmailService
    {
        private readonly ILog log = LogManager.GetLogger(typeof (EmailService));
        private readonly IEmailTemplateService templateService;
        private readonly IEmailRepository emailRepository;

        public EmailService(IEmailTemplateService templateService, IEmailRepository emailRepository)
        {
            Ensure.That(templateService, "templateService").IsNotNull();
            Ensure.That(emailRepository, "emailRepository").IsNotNull();

            this.templateService = templateService;
            this.emailRepository = emailRepository;
        }

        public string RunEmailTemplate(string templateName, object model = null)
        {
            Ensure.That(templateName, "templateName").IsNotNull();

            ITemplate template = templateService.Resolve(templateName, model);
            string body = template.Run();
            return body;
        }

        public void SendEmail(string recipient, string subject, string body)
        {
            MailAddress sender = Config.OutgoingEmail.GetAddress();
            EmailMessageModel email = new EmailMessageModel(sender, recipient, subject, body);
            log.Info(Common.Resources.Debug.EmailOutgoing.FormatWith(recipient, subject));
            emailRepository.Send(email);
        }

        internal void IncludeCommonEmailModelValues(EmailModel model)
        {
            Ensure.That(model, "model").IsNotNull();

            model.FacebookProfileLink = Common.Resources.Links.FacebookProfile;
            model.TwitterProfileLink = Common.Resources.Links.TwitterProfile;
            model.CopyrightYear = DateTime.UtcNow.Year;
            model.LatestNewsSidebarModel = new LatestNewsSidebarModel
            {
                Titles = new[] {"muerte en iran", "vida en israel"} // todo repo get actual posts
            };
        }

        public void SendRegistrationEmail(string recipient, object model)
        {
            Ensure.That(recipient, "recipient").IsNotNull();
            Ensure.That(model, "model").IsNotNull();
            Ensure.ThatTypeFor(model, "model").IsOfType<RegistrationEmailModel>();

            var email = (RegistrationEmailModel)model;

            IncludeCommonEmailModelValues(email);
            email.Subject = Common.Resources.User.EmailRegistrationSubject;
            email.AccountValidationLink = "http://bruttissi.mo/user/validate/alksdjalksjd"; // TODO: provide when invoking sendregistrationemail, model validation?
            string body = RunEmailTemplate(Common.Resources.Email.ValidationTemplate, model);
            SendEmail(recipient, email.Subject, body);
        }
    }
}
