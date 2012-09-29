using Bruttissimo.Common.Resources;
using Bruttissimo.Mvc.Model.ViewModels;
using FluentValidation;
using Regular = Bruttissimo.Common.Resources.Shared.Regular;

namespace Bruttissimo.Mvc.Model.Validators
{
    public class PostCreationModelValidator : AbstractValidator<PostCreationModel>
    {
        public PostCreationModelValidator()
        {
            RuleFor(m => m.Link)
                .NotNull()
                .WithLocalizedMessage(() => Validation.Required);

            RuleFor(m => m.Link)
                .Matches(Regular.WebLink)
                .WithLocalizedMessage(() => Validation.NoLink);
        }
    }
}
