namespace Bruttissimo.Domain.Logic.Email.Model
{
    public class RegistrationEmailModel : EmailModel
    {
        public string DisplayName { get; set; }
        public string AccountValidationLink { get; set; }
    }
}
