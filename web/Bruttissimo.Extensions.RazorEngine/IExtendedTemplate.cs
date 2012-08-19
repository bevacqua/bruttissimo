namespace Bruttissimo.Extensions.RazorEngine
{
    public interface IExtendedTemplate
    {
        TemplateResourceHelper Resource { get; set; }
        TemplateUrlHelper Url { get; set; }
    }
}
