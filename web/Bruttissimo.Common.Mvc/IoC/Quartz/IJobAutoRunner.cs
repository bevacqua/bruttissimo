using Quartz;

namespace Bruttissimo.Common.Mvc
{
	public interface IJobAutoRunner
	{
		void Fire(IScheduler scheduler);
	}
}