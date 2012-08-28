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
        private readonly IMapper mapper;

        public FacebookExportJob(IFacebookRepository facebookService, IMapper mapper)
        {
            if (facebookService == null)
            {
                throw new ArgumentNullException("facebookService");
            }
            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }
            this.facebookService = facebookService;
            this.mapper = mapper;
        }

        public override void DoWork(IJobExecutionContext context)
        {
            Thread.Sleep(2500);
        }
    }
}
