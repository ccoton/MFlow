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
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Equal(u => u.Username, "testing").Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_Equal_Expression()
        {
            var user = new User() { Password = "password123", Username = "testing", ConfirmPassword = "password123" };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Equal(u => u.Password, u=>u.ConfirmPassword, "testing").Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_Not_Equal()
        {
            var user = new User() { Password = "password123", Username = "testing" };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .NotEqual(u => u.Username, "testing").Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_NotEqual_Expression()
        {
            var user = new User() { Password = "password123", Username = "testing", ConfirmPassword = "password1234" };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .NotEqual(u => u.Password, u => u.ConfirmPassword, "testing").Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_Not_Equal_When_Null()
        {
            var user = new User() { Password = "password123", Username = null };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .NotEqual(u => u.Username, "testing").Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_DependsOn_Satisfied_Dependency()
        {
            var user = new User() { Password = "password123", Username = "testing", LoginCount = 12 };

            IFluentValidation<User> dependency = _factory.GetFluentValidation<User>(user);
            dependency.If(true);

            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .GreaterThan(u => u.LoginCount, 11).DependsOn(dependency).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_DependsOn_Unsatisfied_Dependency()
        {
            var user = new User() { Password = "password123", Username = "testing", LoginCount = 12 };

            IFluentValidation<User> dependency = _factory.GetFluentValidation<User>(user);
            dependency.If(false);

            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .GreaterThan(u => u.LoginCount, 11).DependsOn(dependency).Satisfied());
        }
    }
}
