using System;

namespace Bruttissimo.Common.Mvc.HttpModules.Wiring
{
    /// <summary>
    /// Marks an HttpModule as a module intended to be paired with the application pipeline.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ApplicationModuleAttribute : Attribute
    {
        public int Priority { get; private set; }

        /// <summary>
        /// Marks an HttpModule as a module intended to be paired with the application pipeline.
        /// </summary>
        /// <param name="priority">Higher priority means earlier execution.</param>
        public ApplicationModuleAttribute(int priority = 0)
        {
            Priority = priority;
        }
    }
}