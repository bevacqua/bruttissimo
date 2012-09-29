namespace Bruttissimo.Domain.Entity.Entities
{
    public class Picture
    {
        public long Id { get; set; }
        public string Source { get; set; }
        public string Large { get; set; }
        public string Regular { get; set; }
        public string Thumbnail { get; set; }
    }
}
