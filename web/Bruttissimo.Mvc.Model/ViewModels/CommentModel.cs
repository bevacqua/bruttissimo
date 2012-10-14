using System.Web;

namespace Bruttissimo.Mvc.Model.ViewModels
{
    public class CommentModel
    {
        public long Id { get; set; }
        public IHtmlString Message { get; set; }
    }
}
