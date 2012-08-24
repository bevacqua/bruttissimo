using FluentValidation;

namespace Bruttissimo.Mvc.Model
{
    public class PostCreationModelValidator : AbstractValidator<PostCreationModel>
    {
        public PostCreationModelValidator()
        {
            RuleFor(m => m.Link)
                .NotNull()
                .WithLocalizedMessage(() => Common.Resources.Validation.Required);

            RuleFor(m => m.Link)
                .Matches(Common.Resources.Shared.Regex.WebLink)
                .WithLocalizedMessage(() => Common.Resources.Validation.NoLink);
        }
    }
}
