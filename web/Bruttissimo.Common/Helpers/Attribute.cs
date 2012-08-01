using System;
using System.Linq;
using System.Reflection;

namespace Bruttissimo.Common
{
	/// <summary>
	/// Reflection helpers to help deal with attributes.
	/// </summary>
	public static class AttributeHelpers
	{
		/// <summary>
		/// Returns the first occurrence of a given attribute T on an attribute provider.
		/// </summary>
		/// <typeparam name="T">The type of the attribute to look for.</typeparam>
		/// <param name="instance">The target type.</param>
		/// <returns>The attribute instance on the target, if any.</returns>
        public static T GetAttribute<T>(this ICustomAttributeProvider instance) where T : Attribute
		{
            return instance.GetCustomAttributes(typeof(T), true).Cast<T>().FirstOrDefault();
		}

		/// <summary>
		/// Returns the first occurrence of a given attribute T on an Enum value.
		/// </summary>
		/// <typeparam name="T">The type of the attribute to look for.</typeparam>
		/// <param name="value">The enum instance.</param>
		/// <returns>The attribute instance on the target, if any.</returns>
		public static T GetAttribute<T>(this Enum value) where T : Attribute
		{
			Type type = value.GetType();
			MemberInfo[] member = type.GetMember(Enum.GetName(type, value));
			return member[0].GetAttribute<T>();
		}
	}
}
