using Bruttissimo.Common.Interface;
using RazorEngine.Text;

namespace Bruttissimo.Extensions.RazorEngine.Interface
{
    public interface ITemplateResourceHelper : IResourceHelper<IEncodedString>
    {
        string ManualCacheKeyOverride { set; }
    }
}