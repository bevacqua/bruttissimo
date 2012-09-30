using Bruttissimo.Common.Guard;
using Castle.Windsor;

namespace Bruttissimo.Common.InversionOfControl
{
    internal sealed class ContainerAccessor : IContainerAccessor
    {
        private readonly IWindsorContainer container;

        public IWindsorContainer Container
        {
            get { return container; }
        }

        public ContainerAccessor(IWindsorContainer container)
        {
            Ensure.That(() => container).IsNotNull();
            this.container = container;
        }
    }
}
