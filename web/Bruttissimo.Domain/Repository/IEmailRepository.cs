namespace Bruttissimo.Domain
{
	public interface IEmailRepository
	{
		void Send(EmailMessageModel model);
	}
}