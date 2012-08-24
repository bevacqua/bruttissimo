using System;
using System.Globalization;

namespace Bruttissimo.Common
{
    public static class ConvertibleExtensions
    {
        public static string ToInvariantString(this IConvertible convertible)
        {
            return convertible.ToString(CultureInfo.InvariantCulture);
        }
    }
}
