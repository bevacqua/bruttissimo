using System;
using Bruttissimo.Common.Guard;
using SignalR.Hubs;

namespace Bruttissimo.Common.Mvc
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
