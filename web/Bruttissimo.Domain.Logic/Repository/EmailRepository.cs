using System.Net.Mail;
using Bruttissimo.Common;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Resources;
using Bruttissimo.Common.Static;
using Bruttissimo.Domain.Entity.Email;
using Bruttissimo.Domain.Repository;
using log4net;

namespace Bruttissimo.Domain.Logic.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly ILog log = LogManager.GetLogger(typeof (EmailRepository));
        private readonly SmtpClient client;

        public EmailRepository(SmtpClient client)
        {
            Ensure.That(client, "client").IsNotNull();

            this.client = client;
        }

        public void Send(EmailMessageModel model)
        {
            Ensure.That(model, "model").IsNotNull();

            MailMessage message = new MailMessage(model.Sender, model.Recipient)
            {
                Subject = model.Subject,
                Body = model.Body,
                IsBodyHtml = true
            };
            try
            {
                using (message)
                {
                    if (Config.OutgoingEmail.Timeout.HasValue)
                    {
                        client.Timeout = Config.OutgoingEmail.Timeout.Value;
                    }
                    client.Send(message); // TODO: SendAsync?
                }
            }
            catch (SmtpException exception)
            {
                log.Warn(Error.MailSendError, exception);
            }
        }
    }
}
