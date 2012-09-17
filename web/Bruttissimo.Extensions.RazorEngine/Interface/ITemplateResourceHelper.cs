using Bruttissimo.Common;
using RazorEngine.Text;

namespace Bruttissimo.Extensions.RazorEngine
{
    public interface ITemplateResourceHelper : IResourceHelper<IEncodedString>
    {
        string ManualCacheKeyOverride { set; }
    }
}