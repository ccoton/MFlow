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
        public void Test_Fluent_Validation_Equal()
        {
            var user = new User() { Password = "password123", Username = "testing" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.Username).IsEqual("testing").Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_Equal_Expression()
        {
            var user = new User() { Password = "password123", Username = "testing", ConfirmPassword = "password123" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.Password).IsEqual(u=>u.ConfirmPassword).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_Not_Equal()
        {
            var user = new User() { Password = "password123", Username = "testing" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Username).IsNotEqual("testing").Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_NotEqual_Expression()
        {
            var user = new User() { Password = "password123", Username = "testing", ConfirmPassword = "password1234" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.Password).IsNotEqual(u => u.ConfirmPassword).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_Not_Equal_When_Null()
        {
            var user = new User() { Password = "password123", Username = null };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Username).IsNotEqual("testing").Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_DependsOn_Satisfied_Dependency()
        {
            var user = new User() { Password = "password123", Username = "testing", LoginCount = 12 };

            var dependency = _factory.GetFluentValidation<User>(user);
            dependency.If(true);

            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.LoginCount).IsGreaterThan(11).DependsOn(dependency.If(true)).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_DependsOn_Unsatisfied_Dependency()
        {
            var user = new User() { Password = "password123", Username = "testing", LoginCount = 12 };

            var dependency  = _factory.GetFluentValidation<User>(user);

            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.LoginCount).IsGreaterThan(11).DependsOn(dependency.If(false)).Satisfied());
        }
    }
}
