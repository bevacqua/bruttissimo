using Bruttissimo.Common;
using Bruttissimo.Common.Guard;
using Quartz;

namespace Bruttissimo.Domain.Logic
{
    // [AutoRun]
    [DisallowConcurrentExecution]
    public class TwitterExportJob : BaseJob
    {
        private readonly ITwitterService twitterService;

        public TwitterExportJob(ITwitterService twitterService)
        {
            Ensure.That(twitterService, "twitterService").IsNotNull();

            this.twitterService = twitterService;
        }

        public override void DoWork(IJobExecutionContext context)
        {
            twitterService.Export();
        }
    }
}
