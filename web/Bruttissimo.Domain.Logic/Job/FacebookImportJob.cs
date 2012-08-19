using System;
using Bruttissimo.Common;
using Castle.MicroKernel;
using Quartz;

namespace Bruttissimo.Domain.Logic
{
    // [AutoRun]
    public class FacebookImportJob : BaseJob
    {
        private readonly IFacebookService facebookService;

        public FacebookImportJob(IKernel kernel, IFacebookService facebookService)
            : base(kernel, facebookService)
        {
            if (facebookService == null)
            {
                throw new ArgumentNullException("facebookService");
            }
            this.facebookService = facebookService;
        }

        public override void DoWork(IJobExecutionContext context)
        {
            string feed = Config.Social.FacebookFeedId; // TODO: how to pass feed Id to a job?
            facebookService.Import(feed);
        }
    }
}
