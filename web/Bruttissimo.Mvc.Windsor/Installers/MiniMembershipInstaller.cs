using System.Web;
using System.Web.Security;
using Bruttissimo.Domain.Logic.MiniMembership;
using Bruttissimo.Domain.MiniMembership;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Mvc.Windsor.Installers
{
    /// <summary>
    /// Installs MiniMembership implementation components.
    /// </summary>
    public class MiniMembershipInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component
                    .For<MembershipProvider>()
                    .ImplementedBy<MiniMembershipProvider>()
                    .LifestylePerWebRequest()
                );

            container.Register(
                Component
                    .For<RoleProvider>()
                    .ImplementedBy<MiniRoleProvider>()
                    .LifestylePerWebRequest()
                );
            container.Register(
                Component
                    .For<MiniAuthentication>()
                    .ImplementedBy<MiniAuthentication>()
                    .LifestylePerWebRequest()
                );

            container.Register(
                Component
                    .For<IMiniAuthentication>()
                    .ImplementedBy<MiniAuthenticationWrapper>()
                    .LifestylePerWebRequest()
                );

            container.Register(
                Component
                    .For<IHttpModule>()
                    .ImplementedBy<MiniAuthenticationModule>()
                    .LifestyleTransient()
                );
        }
    }
}
