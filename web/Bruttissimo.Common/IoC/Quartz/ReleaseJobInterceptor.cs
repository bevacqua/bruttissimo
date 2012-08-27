using Castle.MicroKernel;

namespace Bruttissimo.Common
{
    public class ReleaseJobInterceptor : ReleaseComponentInterceptor<BaseJob>
    {
        public ReleaseJobInterceptor(IKernel kernel)
            : base(kernel)
        {
        }
    }
}