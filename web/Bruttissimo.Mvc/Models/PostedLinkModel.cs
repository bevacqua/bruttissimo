using Bruttissimo.Common;

namespace Bruttissimo.Mvc.Models
{
    public class PostedLinkModel : PostModel
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public string PictureUrl { get; set; }
        public bool HasPicture { get { return !PictureUrl.NullOrEmpty(); } }
    }
}