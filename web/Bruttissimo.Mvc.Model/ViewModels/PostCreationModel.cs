using FluentValidation.Attributes;

namespace Bruttissimo.Mvc.Model
{
	[Validator(typeof(PostCreationModelValidator))]
	public class PostCreationModel
    {
        public string Link { get; set; }
        public string UserMessage { get; set; }
	}
}