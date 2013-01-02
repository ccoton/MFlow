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
        public void Test_Fluent_Validation_RegEx()
        {
            var user = new User() { Password = "password123", Username = "ausername@somedomain.com", LoginCount = 12 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.Username).Mathes(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*").Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_RegEx_When_Null()
        {
            var user = new User() { Password = "password123", Username = null, LoginCount = 12 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Username).Mathes(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*").Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_IsEmail_With_Valid_Value()
        {
            var user = new User() { Password = "password123", Username = "ausername@somedomain.com", LoginCount = 12 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.Username).IsEmail().Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_IsEmail_With_InValid_Value()
        {
            var user = new User() { Password = "password123", Username = "ausername", LoginCount = 12 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Username).IsEmail().Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_IsEmail_When_Null()
        {
            var user = new User() { Password = "password123", Username = null, LoginCount = 12 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Username).IsEmail().Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_Contains_With_Valid_Value()
        {
            var user = new User() { Password = "password123", Username = "ausername@somedomain.com", LoginCount = 12 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.Username).Contains("username").Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_Contains_With_InValid_Value()
        {
            var user = new User() { Password = "password123", Username = "ausername", LoginCount = 12 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Username).Contains("testing").Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_Contains_When_Null()
        {
            var user = new User() { Password = "password123", Username = null, LoginCount = 12 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Username).Contains("test").Satisfied());
        }
    }
}
