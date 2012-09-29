using System.Diagnostics;
using Bruttissimo.Common.Extensions;
using Bruttissimo.Common.Guard.Resources;

namespace Bruttissimo.Common.Guard
{
    public static class EnsureDoubleExtensions
    {
        [DebuggerStepThrough]
        public static Param<double> IsLt(this Param<double> param, double limit)
        {
            if (param.Value >= limit)
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotLt.FormatWith(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<double> IsLte(this Param<double> param, double limit)
        {
            if (!(param.Value <= limit))
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotLte.FormatWith(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<double> IsGt(this Param<double> param, double limit)
        {
            if (param.Value <= limit)
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotGt.FormatWith(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<double> IsGte(this Param<double> param, double limit)
        {
            if (!(param.Value >= limit))
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotGte.FormatWith(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<double> IsInRange(this Param<double> param, double min, double max)
        {
            if (param.Value < min)
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotInRange_TooLow.FormatWith(param.Value, min));

            if (param.Value > max)
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotInRange_TooHigh.FormatWith(param.Value, max));

            return param;
        }
    }
}