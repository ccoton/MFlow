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
        public void Test_Fluent_Validation_LessThan()
        {
            var user = new User() { Password = "password123", Username = "testing", LoginCount = 10 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.LoginCount).IsLessThan(11).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_LessThanOrEqualTo()
        {
            var user = new User() { Password = "password123", Username = "testing", LoginCount = 10 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.LoginCount).IsLessThanOrEqualTo(11)
                .Check(u => u.LoginCount).IsLessThanOrEqualTo(10).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_GreaterThan()
        {
            var user = new User() { Password = "password123", Username = "testing", LoginCount = 12 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.LoginCount).IsGreaterThan(11).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_GreaterThanOrEqualTo()
        {
            var user = new User() { Password = "password123", Username = "testing", LoginCount = 12 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.LoginCount).IsGreaterThanOrEqualTo(12)
                .Check(u => u.LoginCount).IsGreaterThanOrEqualTo(11).Satisfied());
        }
    }
}
