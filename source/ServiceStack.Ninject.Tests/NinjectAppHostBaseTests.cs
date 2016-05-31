using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject.Modules;
using ServiceStack;

namespace Tests
{
    interface IWeapon
    {
        string Attack();
    }
    class Sword : IWeapon
    {

        public string Attack()
        {
            return "Slashing enemy with a sword!";
        }
    }
    class Gun : IWeapon
    {

        public string Attack()
        {
            return "Shooting enemy with a gun!";
        }
    }
    [Route("/api/sample")]
    class SampleRequest : IReturn<string>
    {

    }
    class SampleService : Service
    {
        readonly IWeapon _weapon;
        public SampleService(IWeapon weapon)
        {
            _weapon = weapon;
        }

        public object Any(SampleRequest request)
        {
            return _weapon.Attack();
        }
    }

    class SampleModule : NinjectModule
    {

        public override void Load()
        {
            Bind<IWeapon>().To<Sword>();
        }
    }
    class SampleNinjectAppHost : ServiceStack.Ninject.NinjectAppSelfHostBase
    {
        public SampleNinjectAppHost()
            : base("SampleService", typeof(SampleNinjectAppHost).Assembly)
        {

        }

        public override void Configure(Funq.Container container)
        {
            Kernel.Load(new List<NinjectModule>{new SampleModule()});
            base.Configure(container);            
        }
    }
    /// <summary>
    /// Summary description for NinjectAppHostBaseTests
    /// </summary>
    [TestClass]
    public class NinjectAppHostBaseTests
    {
        public NinjectAppHostBaseTests()
        {
            
        }

        

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void AppHostContainerAdapterShouldResolveIWeapon()
        {
            var port = "1331";
            var appHost = new SampleNinjectAppHost()
                .Init()
                .Start(string.Format("http://*:{0}/", port));

            var weapon = appHost.Container.Adapter.TryResolve<IWeapon>();
            Assert.IsNotNull(weapon);
            Assert.AreEqual("Slashing enemy with a sword!", weapon.Attack());
            Assert.AreEqual(typeof(Sword), weapon.GetType());
            
        }
    }
}
