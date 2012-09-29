using System.Diagnostics;
using Bruttissimo.Common.Extensions;
using Bruttissimo.Common.Guard.Resources;

namespace Bruttissimo.Common.Guard
{
    public static class EnsureDecimalExtensions
    {
        [DebuggerStepThrough]
        public static Param<decimal> IsLt(this Param<decimal> param, decimal limit)
        {
            if (param.Value >= limit)
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotLt.FormatWith(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<decimal> IsLte(this Param<decimal> param, decimal limit)
        {
            if (!(param.Value <= limit))
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotLte.FormatWith(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<decimal> IsGt(this Param<decimal> param, decimal limit)
        {
            if (param.Value <= limit)
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotGt.FormatWith(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<decimal> IsGte(this Param<decimal> param, decimal limit)
        {
            if (!(param.Value >= limit))
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotGte.FormatWith(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<decimal> IsInRange(this Param<decimal> param, decimal min, decimal max)
        {
            if (param.Value < min)
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotInRange_TooLow.FormatWith(param.Value, min));

            if (param.Value > max)
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotInRange_TooHigh.FormatWith(param.Value, max));

            return param;
        }
    }
}