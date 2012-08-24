using Castle.MicroKernel.Registration;

namespace Bruttissimo.Common
{
    public static class CastleWindsorHelpers
    {
        public static ComponentRegistration<T> LifestyleHybridPerWebRequestPerThread<T>(this ComponentRegistration<T> registration) where T : class
        {
            return registration.LifeStyle.HybridPerWebRequestPerThread();
        }

        public static BasedOnDescriptor LifestyleHybridPerWebRequestPerThread(this BasedOnDescriptor registration)
        {
            return registration.Configure(x => x.LifeStyle.HybridPerWebRequestPerThread());
        }
    }
}
