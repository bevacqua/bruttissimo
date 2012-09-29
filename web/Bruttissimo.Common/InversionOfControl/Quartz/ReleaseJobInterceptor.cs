using Castle.MicroKernel;

namespace Bruttissimo.Common.InversionOfControl.Quartz
{
    public class ReleaseJobInterceptor : ReleaseComponentInterceptor<BaseJob>
    {
        public ReleaseJobInterceptor(IKernel kernel)
            : base(kernel)
        {
        }
    }
}