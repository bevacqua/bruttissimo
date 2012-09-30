using Bruttissimo.Common;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Quartz;
using Bruttissimo.Domain.Service;
using Quartz;

namespace Bruttissimo.Domain.Logic.Job
{
    // [AutoRun]
    [DisallowConcurrentExecution]
    public class FacebookExportJob : BaseJob
    {
        private readonly IFacebookService facebookService;

        public FacebookExportJob(IFacebookService facebookService)
        {
            Ensure.That(() => facebookService).IsNotNull();

            this.facebookService = facebookService;
        }

        public override void DoWork(IJobExecutionContext context)
        {
            facebookService.Export();
        }
    }
}
