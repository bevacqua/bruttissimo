using System;
using System.Collections.Generic;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
	public class JobService : IJobService
	{
		private readonly ILogRepository logRepository;

		public LogService(ILogRepository logRepository)
		{
			if (logRepository == null)
			{
				throw new ArgumentNullException("logRepository");
			}
			this.logRepository = logRepository;
		}

	    public IList<JobDto> GetScheduledJobs()
	    {
	        throw new NotImplementedException();
	    }
	}
}