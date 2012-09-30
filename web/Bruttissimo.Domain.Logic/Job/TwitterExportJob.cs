using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Quartz;
using Bruttissimo.Domain.Service;
using Quartz;

namespace Bruttissimo.Domain.Logic.Job
{
    // [AutoRun]
    [DisallowConcurrentExecution]
    public class TwitterExportJob : BaseJob
    {
        private readonly ITwitterService twitterService;

        public TwitterExportJob(ITwitterService twitterService)
        {
            Ensure.That(() => twitterService).IsNotNull();

            this.twitterService = twitterService;
        }

        public override void DoWork(IJobExecutionContext context)
        {
            twitterService.Export();
        }
    }
}
