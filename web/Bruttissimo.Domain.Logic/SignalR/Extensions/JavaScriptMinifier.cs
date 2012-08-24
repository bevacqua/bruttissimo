using System;
using Bruttissimo.Common.Mvc;
using SignalR.Hubs;

namespace Bruttissimo.Domain.Logic
{
    public sealed class JavaScriptMinifier : IJavaScriptMinifier
    {
        private readonly IResourceCompressor resourceCompressor;

        public JavaScriptMinifier(IResourceCompressor resourceCompressor)
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
