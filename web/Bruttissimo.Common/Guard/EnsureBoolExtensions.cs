using System.Diagnostics;
using Bruttissimo.Common.Guard.Resources;

namespace Bruttissimo.Common.Guard
{
    public static class EnsureBoolExtensions
    {
        [DebuggerStepThrough]
        public static Param<bool> IsTrue(this Param<bool> param)
        {
            if (!param.Value)
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotTrue);

            return param;
        }

        [DebuggerStepThrough]
        public static Param<bool> IsFalse(this Param<bool> param)
        {
            if (param.Value)
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotFalse);

            return param;
        }
    }
}