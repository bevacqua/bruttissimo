using System;
using System.Collections.ObjectModel;

namespace Bruttissimo.Common.Mvc
{
    public interface IJobTypeStore
    {
        ReadOnlyCollection<Type> All { get; }
        ReadOnlyCollection<Type> AutoRun { get; }
    }
}