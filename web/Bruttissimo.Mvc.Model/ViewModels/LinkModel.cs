using Bruttissimo.Common;

namespace Bruttissimo.Mvc.Model
{
    public class LinkModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string ReferenceUri { get; set; }

        public bool HasPicture
        {
            get { return !Picture.NullOrEmpty(); }
        }
    }
}
