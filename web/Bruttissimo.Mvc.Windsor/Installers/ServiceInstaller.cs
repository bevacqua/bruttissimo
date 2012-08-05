using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain.Logic;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Mvc
{
	/// <summary>
	/// Registers all services and their support dependency components from Domain Logic.
	/// </summary>
	public class ServiceInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				AllTypes
					.FromAssemblyContaining<EmailService>()
					.Where(t => t.Name.EndsWith("Service"))
					.WithService.Select(IoC.SelectByInterfaceConvention)
					.LifestylePerWebRequest()
			);

			container.Register(
				AllTypes
					.FromAssemblyContaining<AuthenticationPortal>()
					.Where(t => t.Name.EndsWith("AuthenticationPortal"))
					.LifestylePerWebRequest()
			);
		}
	}
}