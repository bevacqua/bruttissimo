using System.Collections.Generic;
using System.Linq;

namespace Bruttissimo.Common
{
	public static class EnumerableExtensions
	{
		/// <summary>
		/// Returns whether the enumerable has at least the provided amount of elements.
		/// </summary>
		public static bool HasAtLeast<T>(this IEnumerable<T> enumerable, int amount)
		{
			return enumerable.Take(amount).Count() == amount;
		}

		/// <summary>
		/// Returns whether the enumerable has at most the provided amount of elements.
		/// </summary>
		public static bool HasAtMost<T>(this IEnumerable<T> enumerable, int amount)
		{
			return enumerable.Take(amount + 1).Count() <= amount;
		}
	}
}