using System;
using MFlow.Core.Conditions;
using MFlow.Core.Events;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using System.Linq;
using System.Threading;
using NUnit.Framework;

namespace MFlow.Core.Tests.Validation
{
    public partial class FluentValidation
    {
        [Test]
        public void Test_Fluent_Validation_LessThan()
        {
            var user = new User() { Password = "password123", Username = "testing", LoginCount = 10 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.LoginCount).IsLessThan(11).Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_LessThanOrEqualTo()
        {
            var user = new User() { Password = "password123", Username = "testing", LoginCount = 10 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.LoginCount).IsLessThanOrEqualTo(11)
                .Check(u => u.LoginCount).IsLessThanOrEqualTo(10).Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_GreaterThan()
        {
            var user = new User() { Password = "password123", Username = "testing", LoginCount = 12 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.LoginCount).IsGreaterThan(11).Satisfied());
        }

        [Test]
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
