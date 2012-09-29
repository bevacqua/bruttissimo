using System.Diagnostics;
using Bruttissimo.Common.Guard.Resources;

namespace Bruttissimo.Common.Guard
{
    public static class EnsureNullableValueTypeExtensions
    {
        [DebuggerStepThrough]
        public static Param<T?> IsNotNull<T>(this Param<T?> param) where T : struct
        {
            if (param.Value == null || !param.Value.HasValue)
                throw ExceptionFactory.CreateForNull(param, Exceptions.EnsureExtensions_IsNotNull);

            return param;
        }

        [DebuggerStepThrough]
        public static Param<T?> IsNull<T>(this Param<T?> param) where T : struct
        {
            if (param.Value != null && param.Value.HasValue)
                throw ExceptionFactory.CreateForNonNull(param, Exceptions.EnsureExtensions_IsNull);

            return param;
        }
    }
}