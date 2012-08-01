using System;
using System.Net.Mail;
using Bruttissimo.Common;
using Bruttissimo.Domain.Logic.Email.Model;
using RazorEngine.Templating;
using log4net;

namespace Bruttissimo.Domain.Logic
{
	public class EmailService : IEmailService
	{
		private readonly ILog log = LogManager.GetLogger(typeof(EmailService));
		private readonly IEmailTemplateService templateService;
		private readonly IEmailRepository emailRepository;

		public EmailService(IEmailTemplateService templateService, IEmailRepository emailRepository)
		{
			if (templateService == null)
			{
				throw new ArgumentNullException("templateService");
			}
			if (emailRepository == null)
			{
				throw new ArgumentNullException("emailRepository");
			}
			this.templateService = templateService;
			this.emailRepository = emailRepository;
		}

		public string RunEmailTemplate(string templateName, object model = null)
		{
			if (templateName == null)
			{
				throw new ArgumentNullException("templateName");
			}
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
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			model.FacebookProfileLink = Common.Resources.Links.FacebookProfile;
			model.TwitterProfileLink = Common.Resources.Links.TwitterProfile;
			model.CopyrightYear = DateTime.UtcNow.Year;
			model.LatestNewsSidebarModel = new LatestNewsSidebarModel
			{
				Titles = new[] { "muerte en iran", "vida en israel" } // todo repo get actual posts
			};
		}

		public void SendRegistrationEmail(string recipient, object model)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			RegistrationEmailModel email = model as RegistrationEmailModel;
			if (email == null)
			{
				throw new ArgumentException("Expected model type is RegistrationEmailModel");
			}
			IncludeCommonEmailModelValues(email);
			email.Subject = Common.Resources.User.EmailRegistrationSubject;
			email.AccountValidationLink = "http://bruttissi.mo/user/validate/alksdjalksjd"; // TODO: provide when invoking sendregistrationemail, model validation?
			string body = RunEmailTemplate(Common.Resources.Email.ValidationTemplate, model);
			SendEmail(recipient, email.Subject, body);
		}
	}
}