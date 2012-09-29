using Bruttissimo.Common.Extensions;
using Bruttissimo.Common.Resources;
using Bruttissimo.Mvc.Model.ViewModels;
using FluentValidation;

namespace Bruttissimo.Mvc.Model.Validators
{
    public class UserLoginModelValidator : AbstractValidator<UserLoginModel>
    {
        public UserLoginModelValidator()
        {
            RuleFor(m => m.Email)
                .EmailAddress()
                .WithLocalizedMessage(() => Validation.Email);

            RuleFor(m => m.Password)
                .Length(4, 60)
                .WithLocalizedMessage(() => Validation.PasswordLength)
                .Unless(m => m.Password.NullOrEmpty());

            RuleFor(m => m.Email)
                .NotNull()
                .WithLocalizedMessage(() => Validation.Required)
                .When(m => m.Source == AuthenticationSource.Local);

            RuleFor(m => m.Password)
                .NotNull()
                .WithLocalizedMessage(() => Validation.Required)
                .When(m => m.Source == AuthenticationSource.Local);

            RuleFor(m => m.AccessToken)
                .NotNull()
                .WithLocalizedMessage(() => Validation.Required)
                .When(m => m.Source == AuthenticationSource.Facebook);

            RuleFor(m => m.UserId)
                .NotNull()
                .WithLocalizedMessage(() => Validation.Required)
                .When(m => m.Source == AuthenticationSource.Facebook);

            RuleFor(m => m.UserId)
                .NotNull()
                .WithLocalizedMessage(() => Validation.Required)
                .When(m => m.Source == AuthenticationSource.Twitter);
        }
    }
}
