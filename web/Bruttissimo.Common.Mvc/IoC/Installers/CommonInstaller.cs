using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Common.Mvc
{
	/// <summary>
	/// Registers common dependencies and components.
	/// </summary>
	public sealed class CommonInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
			   Component
				   .For<ILazyComponentLoader>()
				   .ImplementedBy<LazyOfTComponentLoader>()
			);
		}
	}
}