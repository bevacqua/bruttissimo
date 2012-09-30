using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Quartz;
using Bruttissimo.Common.Static;
using Bruttissimo.Domain.Service;
using Quartz;

namespace Bruttissimo.Domain.Logic.Job
{
    [AutoRun]
    [DisallowConcurrentExecution]
    public class FacebookImportJob : BaseJob
    {
        private readonly IFacebookService facebookService;

        public FacebookImportJob(IFacebookService facebookService)
        {
            Ensure.That(() => facebookService).IsNotNull();

            this.facebookService = facebookService;
        }

        public override void DoWork(IJobExecutionContext context)
        {
            string feed = Config.Social.FacebookFeedId;
            facebookService.Import(feed);
        }
    }
}
