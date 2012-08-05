using System;
using System.Net.Mail;

namespace Bruttissimo.Domain.Entity
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
			if (sender == null)
			{
				throw new ArgumentNullException("sender");
			}
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			if (subject == null)
			{
				throw new ArgumentNullException("subject");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			Sender = sender;
			Recipient = new MailAddress(recipient);
			Subject = subject;
			Body = body;
		}
	}
}