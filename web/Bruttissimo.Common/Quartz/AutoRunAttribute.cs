using System;

namespace Bruttissimo.Common
{
    /// <summary>
    /// Jobs decorated with this attribute should be fired when the application starts.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class AutoRunAttribute : Attribute
    {
    }
}
