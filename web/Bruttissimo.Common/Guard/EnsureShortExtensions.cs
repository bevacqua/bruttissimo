using System.Diagnostics;
using Bruttissimo.Common.Extensions;
using Bruttissimo.Common.Guard.Resources;

namespace Bruttissimo.Common.Guard
{
    public static class EnsureShortExtensions
    {
        [DebuggerStepThrough]
        public static Param<short> IsLt(this Param<short> param, short limit)
        {
            if (param.Value >= limit)
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotLt.FormatWith(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<short> IsLte(this Param<short> param, short limit)
        {
            if (!(param.Value <= limit))
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotLte.FormatWith(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<short> IsGt(this Param<short> param, short limit)
        {
            if (param.Value <= limit)
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotGt.FormatWith(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<short> IsGte(this Param<short> param, short limit)
        {
            if (!(param.Value >= limit))
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotGte.FormatWith(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<short> IsInRange(this Param<short> param, short min, short max)
        {
            if (param.Value < min)
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotInRange_TooLow.FormatWith(param.Value, min));

            if (param.Value > max)
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotInRange_TooHigh.FormatWith(param.Value, max));

            return param;
        }
    }
}