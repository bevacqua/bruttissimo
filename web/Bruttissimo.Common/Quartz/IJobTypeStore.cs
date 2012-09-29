using System;
using System.Collections.ObjectModel;

namespace Bruttissimo.Common.Quartz
{
    public interface IJobTypeStore
    {
        ReadOnlyCollection<Type> All { get; }
        ReadOnlyCollection<Type> AutoRun { get; }
    }
}
