using System;
using Bruttissimo.Common.InversionOfControl;
using Castle.Windsor;

namespace Bruttissimo.Common.Mvc.InversionOfControl.Membership
{
    public class WindsorRoleProviderMvc : WindsorRoleProvider
    {
        private readonly Lazy<IWindsorContainer> container;

        protected internal override Lazy<IWindsorContainer> Container
        {
            get { return container; }
        }

        public WindsorRoleProviderMvc()
        {
            container = new Lazy<IWindsorContainer>(() => IoC.Container);
        }
    }
}
