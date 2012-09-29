using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Mvc.Interface;
using SignalR.Hubs;

namespace Bruttissimo.Common.Mvc.SignalR.Extensions
{
    public sealed class HubJavaScriptMinifier : IJavaScriptMinifier
    {
        private readonly IResourceCompressor resourceCompressor;

        public HubJavaScriptMinifier(IResourceCompressor resourceCompressor)
        {
            Ensure.That(resourceCompressor, "resourceCompressor").IsNotNull();

            this.resourceCompressor = resourceCompressor;
        }

        public string Minify(string source)
        {
            string minified = resourceCompressor.MinifyJavaScript(source, false);
            return minified;
        }
    }
}
