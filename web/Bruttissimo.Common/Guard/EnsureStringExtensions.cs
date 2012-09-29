using System.Diagnostics;
using System.Text.RegularExpressions;
using Bruttissimo.Common.Extensions;
using Bruttissimo.Common.Guard.Resources;

namespace Bruttissimo.Common.Guard
{
    public static class EnsureStringExtensions
    {
        [DebuggerStepThrough]
        public static Param<string> IsNotNullOrWhiteSpace(this Param<string> param)
        {
            if (string.IsNullOrWhiteSpace(param.Value))
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotNullOrWhiteSpace);

            return param;
        }

        [DebuggerStepThrough]
        public static Param<string> IsNotNullOrEmpty(this Param<string> param)
        {
            if (string.IsNullOrEmpty(param.Value))
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotNullOrEmpty);

            return param;
        }

        [DebuggerStepThrough]
        public static Param<string> HasLengthBetween(this Param<string> param, int minLength, int maxLength)
        {
            if (string.IsNullOrEmpty(param.Value))
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotNullOrEmpty);

            var length = param.Value.Length;

            if (length < minLength)
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotInRange_String.FormatWith(minLength, maxLength, length));

            if (length > maxLength)
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotInRange_String.FormatWith(minLength, maxLength, length));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<string> Matches(this Param<string> param, string match)
        {
            return Matches(param, new Regex(match));
        }

        [DebuggerStepThrough]
        public static Param<string> Matches(this Param<string> param, Regex match)
        {
            if (!match.IsMatch(param.Value))
            {
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_NoMatch.FormatWith(param.Value, match));
            }
            return param;
        }
    }
}
