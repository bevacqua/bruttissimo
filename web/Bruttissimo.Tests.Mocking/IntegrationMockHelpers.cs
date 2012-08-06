using System.Web;
using System.Web.Routing;
using Bruttissimo.Mvc;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace Bruttissimo.Tests.Mocking
{
	public static class IntegrationMockHelpers
	{
		public static IWindsorContainer GetWindsorContainer()
		{
			IWindsorContainer container = new WindsorContainer();
			container.Kernel.ComponentModelCreated += delegate(ComponentModel model) // avoid issues raised due to an Http Module not being registered.
			{
				if (model.LifestyleType == LifestyleType.PerWebRequest)
					model.LifestyleType = LifestyleType.Transient;
			};
			container.Install(
				new ApplicationInstaller()
			);
			OverrideRegistrations(container);
			return container;
		}

		internal static void OverrideRegistrations(IWindsorContainer container)
		{
			// avoid issues due to HttpContext.Current being null, use a mock instead.
			HttpContextBase mock = MvcMockHelpers.FakeHttpContext();

			container.Register(
				Component
					.For<HttpContextBase>()
					.UsingFactoryMethod(() => mock)
					.LifestyleTransient()
					.OverridesExistingRegistration()
			);

			container.Register(
				Component
					.For<HttpRequestBase>()
					.UsingFactoryMethod(() => mock.Request)
					.LifestyleTransient()
					.OverridesExistingRegistration()
			);

			container.Register(
				Component
					.For<RequestContext>()
					.UsingFactoryMethod(() => mock.Request.RequestContext)
					.LifestyleTransient()
					.OverridesExistingRegistration()
			);

			container.Register(
				Component
					.For<OpenIdRelyingParty>()
					.UsingFactoryMethod(InstanceOpenIdRelyingParty)
					.LifestyleTransient()
					.OverridesExistingRegistration()
			);
		}

		private static OpenIdRelyingParty InstanceOpenIdRelyingParty()
		{
			IOpenIdApplicationStore store = new StandardRelyingPartyApplicationStore(); // use an In-Memory ApplicationStore because of dependency on HttpContext.
			OpenIdRelyingParty party = new OpenIdRelyingParty(store);
			return party;
		}
	}
}