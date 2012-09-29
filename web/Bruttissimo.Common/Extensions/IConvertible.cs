using System;
using System.Globalization;

namespace Bruttissimo.Common.Extensions
{
    public static class ConvertibleExtensions
    {
        public static string ToInvariantString(this IConvertible convertible)
        {
            return convertible.ToString(CultureInfo.InvariantCulture);
        }
    }
}
