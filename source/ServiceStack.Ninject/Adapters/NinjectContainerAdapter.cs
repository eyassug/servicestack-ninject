using Ninject;
using ServiceStack.Configuration;
using System;

namespace ServiceStack.Ninject.Adapters
{
    /// <summary>
    /// An implementation of IContainerAdapter that encapsulates a Ninject kernel to resolve types
    /// </summary>
    public class NinjectContainerAdapter : IContainerAdapter
    {
        private readonly IKernel _kernel;
        public NinjectContainerAdapter(IKernel kernel)
        {
            if (kernel == null)
                throw new ArgumentNullException("kernel");
            _kernel = kernel;
        }
        public T Resolve<T>()
        {
            return _kernel.Get<T>();
        }

        public T TryResolve<T>()
        {
            return _kernel.CanResolve<T>() ? Resolve<T>() : default(T);
        }
    }
}
