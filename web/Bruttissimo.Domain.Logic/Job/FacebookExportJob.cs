using Bruttissimo.Common;
using Bruttissimo.Common.Guard;
using Quartz;

namespace Bruttissimo.Domain.Logic
{
    // [AutoRun]
    [DisallowConcurrentExecution]
    public class FacebookExportJob : BaseJob
    {
        private readonly IFacebookService facebookService;

        public FacebookExportJob(IFacebookService facebookService)
        {
            Ensure.That(facebookService, "facebookService").IsNotNull();

            this.facebookService = facebookService;
        }

        public override void DoWork(IJobExecutionContext context)
        {
            facebookService.Export();
        }
    }
}
