using System;
using System.Linq;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Helpers;
using Castle.MicroKernel;
using FluentValidation;
using FluentValidation.Attributes;

namespace Bruttissimo.Common.Mvc.InversionOfControl.Mvc
{
    internal sealed class WindsorValidatorFactory : ValidatorFactoryBase
    {
        private readonly IKernel kernel;

        public WindsorValidatorFactory(IKernel kernel)
        {
            Ensure.That(kernel, "kernel").IsNotNull();

            this.kernel = kernel;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            Ensure.That(validatorType, "validatorType").IsNotNull();

            Ensure.That(() => validatorType.IsGenericType && validatorType.GetGenericTypeDefinition() == typeof(IValidator<>))
                  .WithExtraMessage(() => "validatorType must implement IValidator<>")
                  .IsTrue();
            
            Type modelType = validatorType.GetGenericArguments().Single();
            ValidatorAttribute validatorAttribute = modelType.GetAttribute<ValidatorAttribute>();
            if (validatorAttribute == null) // if a model doesn't have a validator attribute, that model type shouldn't be validated.
            {
                return kernel.Resolve<IValidator<dynamic>>(); // we resolve a generic null validator in this case.
            }
            else
            {
                return (IValidator)kernel.Resolve(validatorType); // resolve the validator implementation.
            }
        }
    }
}
