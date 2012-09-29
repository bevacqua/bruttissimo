using System;
using System.Diagnostics;
using Bruttissimo.Common.Guard.Resources;

namespace Bruttissimo.Common.Guard
{
    public static class EnsureDateTimeExtensions
    {
        [DebuggerStepThrough]
        public static Param<DateTime> IsLt(this Param<DateTime> param, DateTime limit)
        {
            if (param.Value >= limit)
                throw ExceptionFactory.CreateForParamValidation(param, Exceptions.EnsureExtensions_IsNotLt.FormatWith(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<DateTime> IsLte(this Param<DateTime> param, DateTime limit)
        {
            if (!(param.Value <= limit))
                throw ExceptionFactory.CreateForParamValidation(param, Exceptions.EnsureExtensions_IsNotLte.FormatWith(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<DateTime> IsGt(this Param<DateTime> param, DateTime limit)
        {
            if (param.Value <= limit)
                throw ExceptionFactory.CreateForParamValidation(param, Exceptions.EnsureExtensions_IsNotGt.FormatWith(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<DateTime> IsGte(this Param<DateTime> param, DateTime limit)
        {
            if (!(param.Value >= limit))
                throw ExceptionFactory.CreateForParamValidation(param, Exceptions.EnsureExtensions_IsNotGte.FormatWith(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<DateTime> IsInRange(this Param<DateTime> param, DateTime min, DateTime max)
        {
            if (param.Value < min)
                throw ExceptionFactory.CreateForParamValidation(param, Exceptions.EnsureExtensions_IsNotInRange_TooLow.FormatWith(param.Value, min));

            if (param.Value > max)
                throw ExceptionFactory.CreateForParamValidation(param, Exceptions.EnsureExtensions_IsNotInRange_TooHigh.FormatWith(param.Value, max));

            return param;
        }
    }
}