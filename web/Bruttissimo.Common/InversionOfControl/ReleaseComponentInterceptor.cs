using System;
using System.Linq;
using System.Reflection;
using Bruttissimo.Common.Extensions;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.InversionOfControl.Resources;
using Castle.DynamicProxy;
using Castle.MicroKernel;

namespace Bruttissimo.Common.InversionOfControl
{
    /// <summary>
    /// Interceptor to release components in contexts other than per web request.
    /// NOTE: Dispose must be virtual.
    /// </summary>
    /// <typeparam name="T">The type that implements IDisposable.</typeparam>
    public class ReleaseComponentInterceptor<T> : IInterceptor where T : class
    {
        private static readonly MethodInfo dispose;

        static ReleaseComponentInterceptor()
        {
            Type type = typeof(T);

            bool disposable = type.GetInterfaces().Any(@interface => @interface == typeof(IDisposable));
            if (!disposable)
            {
                throw new NotSupportedException(Exceptions.ReleaseComponentInterceptor_NotSupported.FormatWith(type.Name));
            }

            dispose = typeof(T).GetInterfaceMap(typeof(IDisposable)).TargetMethods.Single();
            if (!dispose.IsVirtual)
            {
                throw new NotSupportedException(Exceptions.ReleaseComponentInterceptor_VirtualNotSupported.FormatWith(type.Name));
            }
        }

        private readonly IKernel kernel;
        private bool released;

        public ReleaseComponentInterceptor(IKernel kernel)
        {
            Ensure.That(() => kernel).IsNotNull();
            this.kernel = kernel;
        }

        public void Intercept(IInvocation invocation)
        {
            if (invocation.Method == dispose)
            {
                if (!released)
                {
                    released = true;
                    kernel.ReleaseComponent(invocation.Proxy);
                }
            }
            else
            {
                invocation.Proceed();
            }
        }
    }
}