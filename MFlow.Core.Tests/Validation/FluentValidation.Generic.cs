using MFlow.Core.Tests.Supporting;
using NUnit.Framework;
using System.Linq;

namespace MFlow.Core.Tests.Validation
{
    public partial class FluentValidation
    {
        [Test]
        public void Test_Fluent_Validation_IsRequired_When_Valid()
        {
            var user = new User {
                Password = "password123"
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                          .Check(u => u.Password).IsRequired<string>().Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsRequired_When_InValid()
        {
            var user = new User {
                Password = ""
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                           .Check(u => u.Password).IsRequired<string>().Satisfied());
        }
        
        [Test]
        public void Test_Fluent_Validation_IsRequired_When_InValid_Returns_Message()
        {
            var user = new User {
                Password = ""
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.AreEqual("Password is a required field", fluentValidation
                           .Check(u => u.Password).IsRequired<string>()
                           .Validate().First().Condition.Message);
        }

        [Test]
        public void Test_Fluent_Validation_DependsOn_Satisfied_Dependency()
        {
            var user = new User {
                LoginCount = 12
            };

            var dependency = _factory.GetFluentValidation<User>(user);
            dependency.If(true);

            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                          .Check(u => u.LoginCount).IsGreaterThan(11).DependsOn(dependency.If(true)).Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_DependsOn_Unsatisfied_Dependency()
        {
            var user = new User {
                LoginCount = 12
            };

            var dependency  = _factory.GetFluentValidation<User>(user);

            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                           .Check(u => u.LoginCount).IsGreaterThan(11).DependsOn(dependency.If(false)).Satisfied());
        }
    }
}
