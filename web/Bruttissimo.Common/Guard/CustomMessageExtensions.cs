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

        public static Param<T> WithExtraMessage<T>(this Param<T> param, string message)
        {
            param.ExtraMessage = () => message;
            return param;
        }

        public static TypeParam WithExtraMessage(this TypeParam param, Func<string> message)
        {
            param.ExtraMessage = message;
            return param;
        }
        public static TypeParam WithExtraMessage(this TypeParam param, string message)
        {
            param.ExtraMessage = () => message;
            return param;
        }
    }
}