using System;
using System.Linq;
using System.Web;
using Castle.Core;
using Castle.MicroKernel;

namespace Bruttissimo.Common.Mvc
{
    /// <summary>
    /// Emits an opinion about a component's lifestyle only if a clear distinction can be made between the available handlers.
    /// </summary>
    public class LifestyleSelector : IHandlerSelector
    {
        public bool HasOpinionAbout(string key, Type service)
        {
            return service != typeof(object); // for some reason, Castle passes typeof(object) if the service type is null.
        }

        public IHandler SelectHandler(string key, Type service, IHandler[] handlers)
        {
            if (CouldHaveAccurateOpinion(handlers))
            {
                if (HttpContext.Current == null)
                {
                    return handlers.Single(x => x.ComponentModel.LifestyleType != LifestyleType.PerWebRequest);
                }
                else
                {
                    return handlers.Single(x => x.ComponentModel.LifestyleType == LifestyleType.PerWebRequest);
                }
            }
            return null; // we don't have an opinion in this case.
        }

        private bool CouldHaveAccurateOpinion(IHandler[] handlers)
        {
            bool two = handlers.Length == 2; // exactly two handlers.
            bool same = handlers.Select(x => x.ComponentModel.Implementation).Distinct().Count() == 1; // exactly the same implementation type.
            bool onePerWebRequest = handlers.Any(x => x.ComponentModel.LifestyleType == LifestyleType.PerWebRequest);

            return two && same && onePerWebRequest;
        }
    }
}