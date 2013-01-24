using MFlow.Core.Tests.Supporting;
using NUnit.Framework;
using System.Linq;

namespace MFlow.Core.Tests.Validation
{
    public partial class FluentValidation
    {
        [Test]
        public void Test_Fluent_Validation_LessThan_Valid()
        {
            var user = new User {
                LoginCount = 10
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                          .Check(u => u.LoginCount).IsLessThan(11).Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_LessThan_InValid()
        {
            var user = new User {
                LoginCount = 12
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                           .Check(u => u.LoginCount).IsLessThan(11).Satisfied());
        }
        
        [Test]
        public void Test_Fluent_Validation_LessThan_InValid_Returns_Message()
        {
            var user = new User {
                LoginCount = 12
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.AreEqual("LoginCount should be less than 11", fluentValidation
                           .Check(u => u.LoginCount).IsLessThan(11)
                           .Validate().First().Condition.Message);
        }

        [Test]
        public void Test_Fluent_Validation_LessThanOrEqualTo_Valid()
        {
            var user = new User {
                LoginCount = 10
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                          .Check(u => u.LoginCount).IsLessThanOrEqualTo(11)
                          .Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_LessThanOrEqualTo_InValid()
        {
            var user = new User {
                LoginCount = 12
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                           .Check(u => u.LoginCount).IsLessThanOrEqualTo(11)
                           .Satisfied());
        }
        
        [Test]
        public void Test_Fluent_Validation_LessThanOrEqualTo_InValid_Returns_Message()
        {
            var user = new User {
                LoginCount = 12
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.AreEqual("LoginCount should be less than or equal to 11", fluentValidation
                           .Check(u => u.LoginCount).IsLessThanOrEqualTo(11)
                           .Validate().First().Condition.Message);
        }

        [Test]
        public void Test_Fluent_Validation_GreaterThan_Valid()
        {
            var user = new User {
                LoginCount = 12
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                          .Check(u => u.LoginCount).IsGreaterThan(11).Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_GreaterThan_InValid()
        {
            var user = new User {
                LoginCount = 10
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                           .Check(u => u.LoginCount).IsGreaterThan(11).Satisfied());
        }
        
        [Test]
        public void Test_Fluent_Validation_GreaterThan_InValid_Returns_Message()
        {
            var user = new User {
                LoginCount = 10
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.AreEqual("LoginCount should be greater than 11", fluentValidation
                           .Check(u => u.LoginCount).IsGreaterThan(11)
                           .Validate().First().Condition.Message);
        }

        [Test]
        public void Test_Fluent_Validation_GreaterThanOrEqualTo_Valid()
        {
            var user = new User {
                LoginCount = 12
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                          .Check(u => u.LoginCount).IsGreaterThanOrEqualTo(12).Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_GreaterThanOrEqualTo_InValid()
        {
            var user = new User {
                LoginCount = 10
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                           .Check(u => u.LoginCount).IsGreaterThanOrEqualTo(11).Satisfied());
        }
        
        [Test]
        public void Test_Fluent_Validation_GreaterThanOrEqualTo_InValid_Returns_Message()
        {
            var user = new User {
                LoginCount = 10
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.AreEqual("LoginCount should be greater than or equal to 11", fluentValidation
                           .Check(u => u.LoginCount).IsGreaterThanOrEqualTo(11)
                           .Validate().First().Condition.Message);
        }
    }
}
