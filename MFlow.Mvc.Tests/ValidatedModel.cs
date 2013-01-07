using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFlow.Mvc.Tests.Supporting;
using NUnit.Framework;

namespace MFlow.Mvc.Tests
{
    [TestFixture]
    public class ValidatedModel
    {
        [Test]
        public void Test_Validated_Model_With_Errors()
        {
            var loginViewModel = new LoginViewModel() { Password = "", Username = "" };
            var results = loginViewModel.Validate(new System.ComponentModel.DataAnnotations.ValidationContext(loginViewModel,null,null));
            Assert.AreEqual(results.Count(), 2);
        }

        [Test]
        public void Test_Validated_Model_With_No_Errors()
        {
            var loginViewModel = new LoginViewModel() { Password = "password", Username = "username" };
			var results = loginViewModel.Validate(new System.ComponentModel.DataAnnotations.ValidationContext(loginViewModel,null,null));
            Assert.AreEqual(results.Count(), 0);
        }
    }
}
