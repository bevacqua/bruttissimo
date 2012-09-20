using System;
using Bruttissimo.Common;
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
            if (twitterService == null)
            {
                throw new ArgumentNullException("twitterService");
            }
            this.twitterService = twitterService;
        }

        public override void DoWork(IJobExecutionContext context)
        {
            twitterService.Export();
        }
    }
}
