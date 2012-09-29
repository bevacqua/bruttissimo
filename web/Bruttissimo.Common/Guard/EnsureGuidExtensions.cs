using System;
using System.Diagnostics;
using Bruttissimo.Common.Guard.Resources;

namespace Bruttissimo.Common.Guard
{
    public static class EnsureGuidExtensions
    {
        [DebuggerStepThrough]
        public static Param<Guid> IsNotEmpty(this Param<Guid> param)
        {
            if (Guid.Empty.Equals(param.Value))
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsEmptyGuid);

            return param;
        }
    }
}