namespace Bruttissimo.Extensions.RazorEngine
{
    public interface IExtendedTemplate
    {
        ITemplateResourceHelper Resource { get; set; }
        TemplateUrlHelper Url { get; set; }
    }
}