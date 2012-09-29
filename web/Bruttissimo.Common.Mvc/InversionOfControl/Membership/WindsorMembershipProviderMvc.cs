using System;
using Bruttissimo.Common.InversionOfControl;
using Castle.Windsor;

namespace Bruttissimo.Common.Mvc
{
    public class WindsorMembershipProviderMvc : WindsorMembershipProvider
    {
        private readonly Lazy<IWindsorContainer> container;

        protected internal override Lazy<IWindsorContainer> Container
        {
            get { return container; }
        }

        public WindsorMembershipProviderMvc()
        {
            container = new Lazy<IWindsorContainer>(() => IoC.Container);
        }
    }
}
