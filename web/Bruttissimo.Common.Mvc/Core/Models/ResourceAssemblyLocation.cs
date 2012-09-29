using System.Reflection;

namespace Bruttissimo.Common.Mvc.Core.Models
{
    public class ResourceAssemblyLocation
    {
        public Assembly Assembly { get; set; }
        public string Namespace { get; set; }
    }
}