using log4net;

namespace Bruttissimo.Domain.Logic
{
	public class FacebookService : IFacebookService
	{
		private readonly ILog log = LogManager.GetLogger(typeof(FacebookService));

		public void Import()
		{
			log.Debug("import job");
		}
	}
}