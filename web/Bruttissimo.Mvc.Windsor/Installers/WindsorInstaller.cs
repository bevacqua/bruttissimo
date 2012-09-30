using System.Collections.Generic;
using System.Reflection;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Common.Mvc.Core.Models;
using Bruttissimo.Common.Mvc.InversionOfControl;
using Bruttissimo.Common.Mvc.InversionOfControl.Installers;
using Bruttissimo.Common.Mvc.InversionOfControl.Mvc;
using Bruttissimo.Domain.Entity;
using Bruttissimo.Domain.Entity.Mappers;
using Bruttissimo.Domain.Logic;
using Bruttissimo.Domain.Logic.Service;
using Bruttissimo.Domain.Logic.SignalR.Hub;
using Bruttissimo.Domain.Social;
using Bruttissimo.Domain.Social.Mappers;
using Bruttissimo.Mvc.Controller;
using Bruttissimo.Mvc.Controller.Controllers;
using Bruttissimo.Mvc.Model;
using Bruttissimo.Mvc.Model.Mappers.Post;
using Bruttissimo.Mvc.Model.ViewModels;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Mvc.Windsor.Installers
{
    /// <summary>
    /// Installs all dependencies into the container.
    /// </summary>
    public class WindsorInstaller : IWindsorInstaller
    {
        private readonly Assembly viewAssembly;
        private readonly string applicationTitle;
        private readonly IList<ResourceAssemblyLocation> resourceAssemblies;

        /// <summary>
        /// Installs all required components and dependencies for the application.
        /// </summary>
        /// <param name="viewAssembly">The view assembly.</param>
        /// <param name="applicationTitle">The default title to display in ajax requests when partially rendering a view.</param>
        /// <param name="resourceAssemblies">The location of the different string resources that are rendered client-side.</param>
        public WindsorInstaller(Assembly viewAssembly, string applicationTitle, IList<ResourceAssemblyLocation> resourceAssemblies)
        {
            Ensure.That(() => viewAssembly).IsNotNull();
            Ensure.That(() => applicationTitle).IsNotNull();
            Ensure.That(() => resourceAssemblies).IsNotNull();

            this.viewAssembly = viewAssembly;
            this.applicationTitle = applicationTitle;
            this.resourceAssemblies = resourceAssemblies;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            MvcInstallerParameters parameters = GetMvcInstallerParameters();

            container.Install(
                new MvcInstaller(parameters),
                new DomainInstaller(),
                new MiniMembershipInstaller(),
                new ServiceInstaller(),
                new RepositoryInstaller(),
                new LibraryInstaller()
            );
        }

        private MvcInstallerParameters GetMvcInstallerParameters()
        {
            Assembly modelAssembly = typeof(UserLoginModel).Assembly;
            Assembly controllerAssembly = typeof(HomeController).Assembly;
            ActionInvokerFilters filters = new ActionInvokerFilters();
            Assembly jobAssembly = typeof(FacebookService).Assembly;
            Assembly[] mapperAssemblies = GetMapperAssemblies();
            Assembly hubAssembly = typeof(LogHub).Assembly;

            MvcInstallerParameters parameters = new MvcInstallerParameters
            (
                modelAssembly,
                viewAssembly,
                controllerAssembly,
                applicationTitle,
                resourceAssemblies,
                filters,
                jobAssembly,
                mapperAssemblies,
                hubAssembly
            );
            return parameters;
        }

        private Assembly[] GetMapperAssemblies()
        {
            //Assembly[] assemblies = AppDomain.CurrentDomain
            //    .GetAssemblies()
            //    .Where(assembly => assembly
            //        .GetTypes()
            //        .Any(type => typeof(IMapperConfigurator).IsAssignableFrom(type))
            //    )
            //    .ToArray();
            Assembly[] assemblies = new []
            {
                typeof(TwitterPostMapper).Assembly,
                typeof(PostMapper).Assembly,
                typeof(PostModelMapper).Assembly
            };
            return assemblies;
        }
    }
}
