using FluentValidation.Attributes;

namespace Bruttissimo.Mvc.Models
{
	[Validator(typeof(PostCreationModelValidator))]
	public class PostCreationModel
    {
        public string Link { get; set; }
        public string UserMessage { get; set; }
	}
}