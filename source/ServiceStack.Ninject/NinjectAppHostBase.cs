using System;
using System.Reflection;
using Ninject;
using ServiceStack.Ninject.Adapters;


namespace ServiceStack.Ninject
{
    public class NinjectAppHostBase : ServiceStack.AppHostBase
    {
        protected readonly IKernel _kernel;
        
        public NinjectAppHostBase(IKernel kernel, string serviceName, params Assembly[] assembliesWithServices)
            : base(serviceName, assembliesWithServices)
        {
            if(kernel == null)
                throw new ArgumentNullException("kernel");
            _kernel = kernel;
        }

        public override void Configure(Funq.Container container)
        {
            container.Adapter = new NinjectContainerAdapter(_kernel);            
        }
    }
}
