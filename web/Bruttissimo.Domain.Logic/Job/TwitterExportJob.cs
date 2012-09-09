using System;
using Bruttissimo.Common;
using Quartz;

namespace Bruttissimo.Domain.Logic
{
    // [AutoRun]
    public class TwitterExportJob : BaseJob
    {
        private readonly IFacebookService facebookService;
        
        public TwitterExportJob(IFacebookService facebookService)
        {
            if (facebookService == null)
            {
                throw new ArgumentNullException("facebookService");
            }
            this.facebookService = facebookService;
        }

        public override void DoWork(IJobExecutionContext context)
        {
            facebookService.Export();
        }
    }
}
