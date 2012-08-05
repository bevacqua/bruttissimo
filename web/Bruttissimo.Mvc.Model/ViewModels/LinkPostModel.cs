using Bruttissimo.Common;

namespace Bruttissimo.Mvc.Model
{
    public class LinkPostModel : PostModel
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public string PictureUrl { get; set; }
        public bool HasPicture { get { return !PictureUrl.NullOrEmpty(); } }
    }
}