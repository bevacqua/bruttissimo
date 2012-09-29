using Bruttissimo.Mvc.Model.Validators;
using FluentValidation.Attributes;

namespace Bruttissimo.Mvc.Model.ViewModels
{
    [Validator(typeof (PostCreationModelValidator))]
    public class PostCreationModel
    {
        public string Link { get; set; }
        public string UserMessage { get; set; }
    }
}
