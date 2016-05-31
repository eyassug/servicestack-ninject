using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests;

namespace ServiceStack.Ninject.Tests
{
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
    
    [TestClass]
    public class ClientIntegrationTests
    {
        [TestMethod]
        public void JsonClientShouldReturnCorrectAttackMessage()
        {
            var port = "1331";
            var appHost = new SampleNinjectAppHost()
                .Init()
                .Start(string.Format("http://*:{0}/", port));
            var client = new JsonServiceClient("http://localhost:1331/");
            var attackMessage = client.Get(new SampleRequest());
            Assert.IsNotNull(attackMessage);
            var expected = new Sword().Attack();
            Assert.AreEqual(expected, attackMessage);
        }
    }
}
