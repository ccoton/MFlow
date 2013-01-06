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
        public void Test_Fluent_Validation_Equal()
        {
            var user = new User() { Password = "password123", Username = "testing" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.Username).IsEqualTo("testing").Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_Equal_Expression()
        {
            var user = new User() { Password = "password123", Username = "testing", ConfirmPassword = "password123" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.Password).IsEqualTo(u=>u.ConfirmPassword).Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_Not_Equal()
        {
            var user = new User() { Password = "password123", Username = "testing" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Username).IsNotEqualTo("testing").Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_NotEqual_Expression()
        {
            var user = new User() { Password = "password123", Username = "testing", ConfirmPassword = "password1234" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.Password).IsNotEqualTo(u => u.ConfirmPassword).Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_Not_Equal_When_Null()
        {
            var user = new User() { Password = "password123", Username = null };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Username).IsNotEqualTo("testing").Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsRequired_When_Valid()
        {
            var user = new User() { Password = "password123", Username = "testing", ConfirmPassword = "password1234" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.Password).IsRequired<string>().Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsRequired_When_Empty()
        {
            var user = new User() { Password = "", Username = "testing", ConfirmPassword = "password1234" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Password).IsRequired<string>().Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_DependsOn_Satisfied_Dependency()
        {
            var user = new User() { Password = "password123", Username = "testing", LoginCount = 12 };

            var dependency = _factory.GetFluentValidation<User>(user);
            dependency.If(true);

            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.LoginCount).IsGreaterThan(11).DependsOn(dependency.If(true)).Satisfied());
        }

        [Test]
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
