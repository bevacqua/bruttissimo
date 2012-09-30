using System;
using Bruttissimo.Common.Guard;

namespace Bruttissimo.Common.Mvc.InversionOfControl.Mvc
{
    /// <summary>
    /// Defines the model type a model binder is in charge of binding.
    /// </summary>
    public sealed class ModelTypeAttribute : Attribute
    {
        public Type ModelType { get; private set; }

        public ModelTypeAttribute(Type modelType)
        {
            Ensure.That(() => modelType).IsNotNull();

            ModelType = modelType;
        }
    }
}
