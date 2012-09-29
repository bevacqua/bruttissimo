using System;

namespace Bruttissimo.Common.Guard
{
    public static class CustomMessageExtensions
    {
        public static Param<T> WithExtraMessage<T>(this Param<T> param, Func<string> message)
        {
            param.ExtraMessage = message;
            return param;
        }
    }
}