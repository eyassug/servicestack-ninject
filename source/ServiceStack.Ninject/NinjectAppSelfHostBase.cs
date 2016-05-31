using System;
using System.Reflection;
using Ninject;
using ServiceStack.Ninject.Adapters;
using Ninject.Modules;


namespace ServiceStack.Ninject
{
    public abstract class NinjectAppSelfHostBase : ServiceStack.AppSelfHostBase
    {
        private readonly IKernel _kernel = new StandardKernel();

        public NinjectAppSelfHostBase(string serviceName, params Assembly[] assembliesWithServices)
            : base(serviceName, assembliesWithServices)
        {

        }
        public NinjectAppSelfHostBase(IKernel kernel, string serviceName, params Assembly[] assembliesWithServices)
            : this(serviceName, assembliesWithServices)
        {
            if(kernel == null)
                throw new ArgumentNullException("kernel");
            _kernel.Load(kernel.GetModules());
        }

        public override void Configure(Funq.Container container)
        {
            container.Adapter = new NinjectContainerAdapter(_kernel);            
        }

        protected IKernel Kernel { get { return _kernel; } }
    }
}
