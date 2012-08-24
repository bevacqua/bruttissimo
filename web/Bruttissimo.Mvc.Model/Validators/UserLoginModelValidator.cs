using Bruttissimo.Common;
using FluentValidation;

namespace Bruttissimo.Mvc.Model
{
    public class UserLoginModelValidator : AbstractValidator<UserLoginModel>
    {
        public UserLoginModelValidator()
        {
            RuleFor(m => m.Email)
                .EmailAddress()
                .WithLocalizedMessage(() => Common.Resources.Validation.Email);

            RuleFor(m => m.Password)
                .Length(4, 60)
                .WithLocalizedMessage(() => Common.Resources.Validation.PasswordLength)
                .Unless(m => m.Password.NullOrEmpty());

            RuleFor(m => m.Email)
                .NotNull()
                .WithLocalizedMessage(() => Common.Resources.Validation.Required)
                .When(m => m.Source == AuthenticationSource.Local);

            RuleFor(m => m.Password)
                .NotNull()
                .WithLocalizedMessage(() => Common.Resources.Validation.Required)
                .When(m => m.Source == AuthenticationSource.Local);

            RuleFor(m => m.AccessToken)
                .NotNull()
                .WithLocalizedMessage(() => Common.Resources.Validation.Required)
                .When(m => m.Source == AuthenticationSource.Facebook);

            RuleFor(m => m.UserId)
                .NotNull()
                .WithLocalizedMessage(() => Common.Resources.Validation.Required)
                .When(m => m.Source == AuthenticationSource.Facebook);

            RuleFor(m => m.UserId)
                .NotNull()
                .WithLocalizedMessage(() => Common.Resources.Validation.Required)
                .When(m => m.Source == AuthenticationSource.Twitter);
        }
    }
}
