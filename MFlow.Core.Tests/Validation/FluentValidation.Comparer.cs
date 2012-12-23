using System;
using MFlow.Core.Conditions;
using MFlow.Core.Events;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;

namespace MFlow.Core.Tests.Validation
{
    public partial class FluentValidation
    {
      
        [TestMethod]
        public void Test_Fluent_Validation_Equals()
        {
            var user = new User() { Password = "password123", Username = "testing" };
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Equals(u => u.Username, "testing").Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_LessThan()
        {
            var user = new User() { Password = "password123", Username = "testing", LoginCount = 10 };
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .LessThan(u => u.LoginCount, 11).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_GreaterThan()
        {
            var user = new User() { Password = "password123", Username = "testing", LoginCount = 12 };
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .GreaterThan(u => u.LoginCount, 11).Satisfied());
        }
    }
}
