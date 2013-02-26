using MFlow.Core.Tests.Supporting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.Validation
{
    [TestFixture]
    public partial class FluentValidation
    {

        [Test]
        public void Test_Fluent_Validation_Collection_Contains_When_Valid()
        {
            var user = new User
            {
                Username = "testing"
            };

            user.Users.Add(user);

            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                          .Check(u => u.Users).Any(user).Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_Collection_Contains_When_InValid()
        {
            var user = new User
            {
                Username = "testing"
            };

            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                          .Check(u => u.Users).Any(user).Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_Collection_Contains_When_InValid_Returns_Message()
        {
            var user = new User
            {
                Password = ""
            };

            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.AreEqual("Users should contain the item specified", fluentValidation
                           .Check(u => u.Users).Any(user)
                           .Validate().First().Condition.Message);
        }

        ///

        [Test]
        public void Test_Fluent_Validation_Collection_None_When_Valid()
        {
            var user = new User
            {
                Username = "testing"
            };

            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                          .Check(u => u.Users).None(user).Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_Collection_None_When_InValid()
        {
            var user = new User
            {
                Username = "testing"
            };

            user.Users.Add(user);

            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                          .Check(u => u.Users).None(user).Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_Collection_None_When_InValid_Returns_Message()
        {
            var user = new User
            {
                Password = ""
            };

            user.Users.Add(user);

            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.AreEqual("Users should not contain the item specified", fluentValidation
                           .Check(u => u.Users).None(user)
                           .Validate().First().Condition.Message);
        }
    }
}
