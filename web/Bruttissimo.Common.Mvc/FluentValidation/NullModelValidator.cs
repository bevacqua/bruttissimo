using FluentValidation;

namespace Bruttissimo.Common.Mvc
{
    /// <summary>
    /// This view model validator is required for all those models where no validator is explicitly declared.
    /// </summary>
    public class NullModelValidator : AbstractValidator<dynamic>
    {
    }
}
