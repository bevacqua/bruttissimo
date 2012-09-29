using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Resources;

namespace Bruttissimo.Common.InversionOfControl
{
    /// <summary>
    /// Reflection helpers to help deal with attributes.
    /// </summary>
    public static class PropertyInjectionHelpers
    {
        /// <summary>
        /// Helper method to remove verbosity from properties intended to be used in dependency injection scenarios.
        /// </summary>
        public static T GetInjectedProperty<T>(this T property, string propertyName) where T : class
        {
            Ensure.That(property, propertyName).IsNotNull();
            return property;
        }

        /// <summary>
        /// Helper method to remove verbosity from properties intended to be used in dependency injection scenarios.
        /// </summary>
        public static T InjectProperty<T>(this T property, T value, string propertyName) where T : class
        {
            Ensure.That(property, propertyName).WithExtraMessage(() => Error.DuplicatePropertyInjection.FormatWith(propertyName)).IsNull();
            Ensure.That(value, propertyName).IsNotNull();

            return value;
        }

        /// <summary>
        /// Helper method to remove verbosity from properties intended to be used in dependency injection scenarios.
        /// </summary>
        public static T? GetInjectedProperty<T>(this T? property, string propertyName) where T : struct
        {
            Ensure.That(property, propertyName).IsNotNull();
            return property;
        }

        /// <summary>
        /// Helper method to remove verbosity from properties intended to be used in dependency injection scenarios.
        /// </summary>
        public static T? InjectProperty<T>(this T? property, T? value, string propertyName) where T : struct
        {
            Ensure.That(property, propertyName).WithExtraMessage(() => Error.DuplicatePropertyInjection.FormatWith(propertyName)).IsNull();
            Ensure.That(value, propertyName).IsNotNull();

            return value;
        }
    }
}
