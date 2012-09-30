using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Bruttissimo.Common.Guard;

namespace Bruttissimo.Common.Quartz
{
    public sealed class JobTypeStore : IJobTypeStore
    {
        private readonly ReadOnlyCollection<Type> allTypes;
        private readonly ReadOnlyCollection<Type> autoRunTypes;

        public ReadOnlyCollection<Type> All
        {
            get { return allTypes; }
        }

        public ReadOnlyCollection<Type> AutoRun
        {
            get { return autoRunTypes; }
        }

        public JobTypeStore(IEnumerable<Type> allTypes, IEnumerable<Type> autoRunTypes)
        {
            Ensure.That(() => allTypes).IsNotNull();
            Ensure.That(() => autoRunTypes).IsNotNull();

            this.allTypes = new ReadOnlyCollection<Type>(allTypes.ToList());
            this.autoRunTypes = new ReadOnlyCollection<Type>(autoRunTypes.ToList());
        }
    }
}
