using System;

namespace Bruttissimo.Common.Guard
{
    public static class ExceptionFactory
    {
        public static ArgumentException Create(Param param, string message)
        {
            return new ArgumentException(GetMessage(param, message), param.Name);
        }

        public static ArgumentNullException CreateForNull(Param param, string message)
        {
            return new ArgumentNullException(param.Name, GetMessage(param, message));
        }

        public static InvalidOperationException CreateForNonNull(Param param, string message)
        {
            return new InvalidOperationException(GetMessage(param, message));
        }

        internal static string GetMessage(Param param, string message)
        {
            if (param.ExtraMessage == null)
            {
                return message;
            }
            else
            {
                return string.Concat(message, Environment.NewLine, param.ExtraMessage());
            }
        }
    }
}