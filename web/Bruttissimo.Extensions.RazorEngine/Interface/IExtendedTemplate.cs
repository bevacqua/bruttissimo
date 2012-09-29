namespace Bruttissimo.Extensions.RazorEngine.Interface
{
    public interface IExtendedTemplate
    {
        ITemplateResourceHelper Resource { get; set; }
        TemplateUrlHelper Url { get; set; }
    }
}
