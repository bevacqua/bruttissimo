using System;
using System.Threading;
using Bruttissimo.Common;
using Quartz;

namespace Bruttissimo.Domain.Logic
{
    [AutoRun]
    public class FacebookExportJob : BaseJob
    {
        private readonly IFacebookRepository facebookService;

        public FacebookExportJob(IFacebookRepository facebookService)
        {
            if (facebookService == null)
            {
                throw new ArgumentNullException("facebookService");
            }
            this.facebookService = facebookService;
        }

        public override void DoWork(IJobExecutionContext context)
        {
            Thread.Sleep(2500);
        }
    }
}
