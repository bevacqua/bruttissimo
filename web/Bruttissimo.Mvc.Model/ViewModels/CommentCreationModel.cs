namespace Bruttissimo.Mvc.Model
{
    public class CommentCreationModel
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string Message { get; set; }
    }
}
