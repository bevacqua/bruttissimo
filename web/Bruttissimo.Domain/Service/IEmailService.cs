namespace Bruttissimo.Domain
{
    public interface IEmailService
    {
        string RunEmailTemplate(string templateName, object model = null);
        void SendEmail(string recipient, string subject, string body);
        void SendRegistrationEmail(string recipient, object model);
    }
}
