using System;
using Castle.MicroKernel;

namespace Bruttissimo.Common
{
    public class LifestyleSelector : IHandlerSelector
    {
        public bool HasOpinionAbout(string key, Type service)
        {
            throw new NotImplementedException();
        }

        public IHandler SelectHandler(string key, Type service, IHandler[] handlers)
        {
            throw new NotImplementedException();
        }
    }
}
