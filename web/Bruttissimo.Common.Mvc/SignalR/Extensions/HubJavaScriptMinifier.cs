using System;
using SignalR.Hubs;

namespace Bruttissimo.Common.Mvc
{
    public sealed class HubJavaScriptMinifier : IJavaScriptMinifier
    {
        private readonly IResourceCompressor resourceCompressor;

        public HubJavaScriptMinifier(IResourceCompressor resourceCompressor)
        {
            if (resourceCompressor == null)
            {
                throw new ArgumentNullException("resourceCompressor");
            }
            this.resourceCompressor = resourceCompressor;
        }

        public string Minify(string source)
        {
            string minified = resourceCompressor.MinifyJavaScript(source, false);
            return minified;
        }
    }
}
