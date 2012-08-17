using System;
using System.Collections.Generic;
using System.Reflection;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain.Entity;
using Bruttissimo.Domain.Logic;
using Bruttissimo.Mvc.Controller;
using Bruttissimo.Mvc.Model;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bruttissimo.Mvc.Windsor
{
    /// <summary>
    /// Installs all dependencies into the container. Acts as the composition root.
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
            if (viewAssembly == null)
            {
                throw new ArgumentNullException("viewAssembly");
            }
            if (applicationTitle == null)
            {
                throw new ArgumentNullException("applicationTitle");
            }
            if (resourceAssemblies == null)
            {
                throw new ArgumentNullException("resourceAssemblies");
            }
            this.viewAssembly = viewAssembly;
            this.applicationTitle = applicationTitle;
            this.resourceAssemblies = resourceAssemblies;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            MvcInstallerParameters parameters = GetMvcInstallerParameters();
            Type[] profileTypes = GetAutoMapperProfileTypes();

            container.Install(
                new MvcInfrastructureInstaller(parameters),
                new AutoMapperInstaller(profileTypes),
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
            MvcInstallerParameters parameters = new MvcInstallerParameters
            (
                modelAssembly,
                viewAssembly,
                controllerAssembly,
                applicationTitle,
                resourceAssemblies,
                filters,
                jobAssembly
            );
            return parameters;
        }

        private Type[] GetAutoMapperProfileTypes()
        {
            Type domainToModel = typeof(DomainToModelProfile);
            Type entityToDto = typeof(EntityToDtoProfile);
            Type[] profileTypes = new[] { entityToDto, domainToModel };
            return profileTypes;
        }
    }
}