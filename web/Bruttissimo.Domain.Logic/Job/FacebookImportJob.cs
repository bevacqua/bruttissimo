using System;
using Bruttissimo.Common;
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
            if (facebookService == null)
            {
                throw new ArgumentNullException("facebookService");
            }
            this.facebookService = facebookService;
        }

        public override bool PreventConcurrentJobInstanceExecution
        {
            get { return true; }
        }

        public override void DoWork(IJobExecutionContext context)
        {
            string feed = Config.Social.FacebookFeedId;
            facebookService.Import(feed);
        }
    }
}
