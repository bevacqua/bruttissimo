using System;

namespace Bruttissimo.Common.Mvc
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ApplicationModuleAttribute : Attribute
    {
        public int Priority { get; private set; }

        public ApplicationModuleAttribute(int priority = 0)
        {
            Priority = priority;
        }
    }
}