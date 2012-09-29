using RazorEngine.Templating;

namespace Bruttissimo.Domain.Logic.Email.RazorEngine
{
    /// <summary>
    /// Template service for emails.
    /// </summary>
    public interface IEmailTemplateService : ITemplateService // this interface simplifies constructor injection.
    {
    }
}
