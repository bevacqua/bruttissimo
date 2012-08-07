using System;
using Bruttissimo.Common.Mvc;
using Quartz;

namespace Bruttissimo.Domain.Logic
{
	[AutoRun]
	public class FacebookImportJob : IJob
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

		public void Execute(IJobExecutionContext context)
		{
			facebookService.Import();
		}
	}
}