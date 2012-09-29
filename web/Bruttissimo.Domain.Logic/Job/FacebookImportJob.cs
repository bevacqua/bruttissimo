using Bruttissimo.Common;
using Bruttissimo.Common.Guard;
using Quartz;

namespace Bruttissimo.Domain.Logic
{
    [AutoRun]
    [DisallowConcurrentExecution]
    public class FacebookImportJob : BaseJob
    {
        private readonly IFacebookService facebookService;

        public FacebookImportJob(IFacebookService facebookService)
        {
            Ensure.That(facebookService, "facebookService").IsNotNull();

            this.facebookService = facebookService;
        }

        public override void DoWork(IJobExecutionContext context)
        {
            string feed = Config.Social.FacebookFeedId;
            facebookService.Import(feed);
        }
    }
}
