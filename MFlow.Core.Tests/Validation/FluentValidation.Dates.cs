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
        public void Test_Fluent_Validation_Before_Valid()
        {
            var user = new User() { LastLogin = DateTime.Now.AddDays(-10),  Password = "password123", Username = "testing", LoginCount = 10 };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Before(u => u.LastLogin, DateTime.Now).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_Before_InValid()
        {
            var user = new User() { LastLogin = DateTime.Now.AddDays(10), Password = "password123", Username = "testing", LoginCount = 10 };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Before(u => u.LastLogin, DateTime.Now).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_After_Valid()
        {
            var user = new User() { LastLogin = DateTime.Now.AddDays(10), Password = "password123", Username = "testing", LoginCount = 10 };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .After(u => u.LastLogin, DateTime.Now).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_After_InValid()
        {
            var user = new User() { LastLogin = DateTime.Now.AddDays(-10), Password = "password123", Username = "testing", LoginCount = 10 };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .After(u => u.LastLogin, DateTime.Now).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_On_Valid()
        {
            var user = new User() { LastLogin = DateTime.Now, Password = "password123", Username = "testing", LoginCount = 10 };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .On(u => u.LastLogin, DateTime.Now).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_On_InValid()
        {
            var user = new User() { LastLogin = DateTime.Now.AddDays(-10), Password = "password123", Username = "testing", LoginCount = 10 };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .On(u => u.LastLogin, DateTime.Now).Satisfied());
        }
    }
}
