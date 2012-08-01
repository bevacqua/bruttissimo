namespace Bruttissimo.Domain.Logic.Email.Model
{
	public class EmailModel
	{
		public string Subject { get; set; }
		public string FacebookProfileLink { get; set; }
		public string TwitterProfileLink { get; set; }
		public int CopyrightYear { get; set; }
		public LatestNewsSidebarModel LatestNewsSidebarModel { get; set; }
	}
}