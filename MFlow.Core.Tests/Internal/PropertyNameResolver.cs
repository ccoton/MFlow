using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MFlow.Core.Tests.Supporting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MFlow.Core.Tests.Internal
{
    [TestClass]
    public class PropertyNameResolver
    {

        [TestMethod]
        public void Test_Property_Resolves_Expression()
        {
            var propertyResolver = new MFlow.Core.Internal.PropertyNameResolver();
            var user = new User() { Username = "testing", Password = "password123" };

            Assert.AreEqual(propertyResolver.Resolve<User, string>(u => u.Username), "Username");
        }

        [TestMethod]
        public void Test_Property_Resolves_Expression_Chain()
        {
            var propertyResolver = new MFlow.Core.Internal.PropertyNameResolver();

            Assert.AreEqual(propertyResolver.Resolve<Thread, string>(t=>t.CurrentCulture.DisplayName), "CurrentCulture.DisplayName");
        }
    }
}
