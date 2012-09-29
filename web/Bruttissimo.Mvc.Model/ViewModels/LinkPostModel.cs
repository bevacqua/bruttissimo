using Bruttissimo.Common.Extensions;

namespace Bruttissimo.Mvc.Model.ViewModels
{
    public class LinkPostModel : PostModel
    {
        public string LinkTitle { get; set; }
        public string LinkDescription { get; set; }
        public string LinkPicture { get; set; }

        public bool HasPicture
        {
            get { return !LinkPicture.NullOrEmpty(); }
        }
    }
}
