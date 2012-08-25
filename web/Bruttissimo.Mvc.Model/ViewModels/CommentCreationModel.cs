using Bruttissimo.Domain;

namespace Bruttissimo.Mvc.Model
{
    public class CommentCreationModel
    {
        public long Id { get; set; }
        public string Comment { get; set; }
        public IMiniPrincipal Principal { get; set; }
    }
}
