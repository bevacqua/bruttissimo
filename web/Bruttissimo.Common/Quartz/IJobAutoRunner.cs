using Quartz;

namespace Bruttissimo.Common
{
    public interface IJobAutoRunner
    {
        void Fire(IScheduler scheduler);
    }
}
