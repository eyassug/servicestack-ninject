# ServiceStack.Ninject
ServiceStack.Ninject is a lightweight library that makes it super-easy to add Ninject to your ServiceStack Applications. It provides an implementation of IContainerAdapter and wrappers for the adapter that inherit from AppHostBase and AppSelfHostBase.

## Usage
#### 1. Loading modules inside the Configure method (using the exposed (read-only) <code>protected IKernel Kernel {get;}</code> property )
<code>
class SampleNinjectAppHost : ServiceStack.Ninject.NinjectAppSelfHostBase

    
    public override void Configure(Funq.Container container)
    {
        Kernel.Load(new List<NinjectModule>{new SampleModule()});
        base.Configure(container);            
    }


</code>

#### 2. Constructor Injection of IKernel Implementation
<code>

public SampleNinjectAppHost() : base(kernel, "SampleService", typeof(SampleNinjectAppHost).Assembly)
        {
        }

</code>
Where the first constructor parameter <code>kernel</code> is of <code>IKernel</code> type, which you can initialise using <code>new StandardKernel(...modules...)</code>
## That's it
I plan to add a NuGet package once I've added more test coverage and configured build on AppVeyor.
