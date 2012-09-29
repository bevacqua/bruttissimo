using System.Diagnostics;
using Bruttissimo.Common.Extensions;
using Bruttissimo.Common.Guard.Resources;

namespace Bruttissimo.Common.Guard
{
    public static class EnsureIntExtensions
    {
        [DebuggerStepThrough]
        public static Param<int> IsLt(this Param<int> param, int limit)
        {
            if (param.Value >= limit)
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotLt.FormatWith(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<int> IsLte(this Param<int> param, int limit)
        {
            if (!(param.Value <= limit))
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotLte.FormatWith(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<int> IsGt(this Param<int> param, int limit)
        {
            if (param.Value <= limit)
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotGt.FormatWith(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<int> IsGte(this Param<int> param, int limit)
        {
            if (!(param.Value >= limit))
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotGte.FormatWith(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<int> IsInRange(this Param<int> param, int min, int max)
        {
            if (param.Value < min)
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotInRange_TooLow.FormatWith(param.Value, min));

            if (param.Value > max)
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotInRange_TooHigh.FormatWith(param.Value, max));

            return param;
        }
    }
}