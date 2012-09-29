namespace Bruttissimo.Mvc.Model.ViewModels
{
    public class CommentCreationModel
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string Message { get; set; }
    }
}
