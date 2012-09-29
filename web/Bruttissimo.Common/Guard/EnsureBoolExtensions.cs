using System;
using System.Diagnostics;
using Bruttissimo.Common.Guard.Resources;

namespace Bruttissimo.Common.Guard
{
    public static class EnsureBoolExtensions
    {
        [DebuggerStepThrough]
        public static Param<bool> IsTrue(this Param<bool> param)
        {
            return param.IsTrueOrThrow<ArgumentException>();
        }

        [DebuggerStepThrough]
        public static Param<bool> IsTrueOrThrow<TException>(this Param<bool> param) where TException : Exception
        {
            if (!param.Value)
                throw ExceptionFactory.Create<TException>(param, Exceptions.EnsureExtensions_IsNotTrue);

            return param;
        }

        [DebuggerStepThrough]
        public static Param<bool> IsFalse(this Param<bool> param)
        {
            return param.IsFalseOrThrow<ArgumentException>();
        }

        [DebuggerStepThrough]
        public static Param<bool> IsFalseOrThrow<TException>(this Param<bool> param) where TException : Exception
        {
            if (param.Value)
                throw ExceptionFactory.Create<TException>(param, Exceptions.EnsureExtensions_IsNotFalse);

            return param;
        }
    }
}