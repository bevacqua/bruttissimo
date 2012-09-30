using System.Net.Mail;
using Bruttissimo.Common.Guard;

namespace Bruttissimo.Domain.Entity.Email
{
    public class EmailMessageModel
    {
        public MailAddress Sender { get; private set; }
        public MailAddress Recipient { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }

        public EmailMessageModel(string sender, string recipient, string subject, string body)
            : this(new MailAddress(sender), recipient, subject, body)
        {
        }

        public EmailMessageModel(MailAddress sender, string recipient, string subject, string body)
        {
            Ensure.That(() => sender).IsNotNull();
            Ensure.That(() => recipient).IsNotNull();
            Ensure.That(() => subject).IsNotNull();
            Ensure.That(() => body).IsNotNull();

            Sender = sender;
            Recipient = new MailAddress(recipient);
            Subject = subject;
            Body = body;
        }
    }
}
