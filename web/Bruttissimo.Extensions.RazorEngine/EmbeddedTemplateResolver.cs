using System;
using System.IO;
using System.Reflection;
using Bruttissimo.Common;
using Bruttissimo.Common.Extensions;
using Bruttissimo.Common.Guard;
using RazorEngine.Templating;

namespace Bruttissimo.Extensions.RazorEngine
{
    /// <summary>
    /// Resolves templates embedded as resources in a target assembly.
    /// </summary>
    public class EmbeddedTemplateResolver : ITemplateResolver
    {
        private readonly Assembly assembly;
        private readonly Type type;
        private readonly string templateNamespace;

        public Assembly ResourceAssembly
        {
            get { return assembly; }
        }

        /// <summary>
        /// Specify an assembly and the template namespace manually.
        /// </summary>
        public EmbeddedTemplateResolver(Assembly assembly, string templateNamespace)
        {
            Ensure.That(() => assembly).IsNotNull();
            Ensure.That(() => templateNamespace).IsNotNull();

            this.assembly = assembly;
            this.templateNamespace = templateNamespace;
        }

        /// <summary>
        /// Uses a type reference to resolve the assembly and namespace where the template resources are embedded.
        /// </summary>
        /// <param name="type">The type whose namespace is used to scope the manifest resource name.</param>
        public EmbeddedTemplateResolver(Type type)
        {
            Ensure.That(() => type).IsNotNull();

            this.assembly = Assembly.GetAssembly(type);
            this.type = type;
        }

        public string Resolve(string name)
        {
            Ensure.That(() => name).IsNotNull();

            Stream stream;
            if (templateNamespace == null)
            {
                stream = assembly.GetManifestResourceStream(type, Common.Resources.RazorEngine.TemplateName.FormatWith(name));
            }
            else
            {
                stream = assembly.GetManifestResourceStream(Common.Resources.RazorEngine.TemplateNameWithNamespace.FormatWith(templateNamespace, name));
            }
            Ensure.That(() => stream).IsNotNull(Common.Resources.RazorEngine.EmbeddedResourceNotFound);

            string template = stream.ReadFully();
            return template;
        }
    }
}
