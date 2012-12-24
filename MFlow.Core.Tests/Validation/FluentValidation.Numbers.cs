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
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .LessThan(u => u.LoginCount, 11).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_LessThanOrEqualTo()
        {
            var user = new User() { Password = "password123", Username = "testing", LoginCount = 10 };
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .LessThanOrEqualTo(u => u.LoginCount, 11)
                .LessThanOrEqualTo(u => u.LoginCount, 10).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_GreaterThan()
        {
            var user = new User() { Password = "password123", Username = "testing", LoginCount = 12 };
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .GreaterThan(u => u.LoginCount, 11).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_GreaterThanOrEqualTo()
        {
            var user = new User() { Password = "password123", Username = "testing", LoginCount = 12 };
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .GreaterThanOrEqualTo(u => u.LoginCount, 12)
                .GreaterThanOrEqualTo(u => u.LoginCount, 11).Satisfied());
        }
    }
}
