using MFlow.Core.Tests.Supporting;
using NUnit.Framework;
using System.Linq;

namespace MFlow.Core.Tests.Validation
{
    public partial class FluentValidation
    {
        
        [Test]
        public void Test_Fluent_Validation_Equal()
        {
            var user = new User {
                Username = "testing"
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                          .Check(u => u.Username).IsEqualTo("testing").Satisfied());
        }
        
        [Test]
        public void Test_Fluent_Validation_Equal_When_InValid()
        {
            var user = new User {
                Username = "testingx"
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                          .Check(u => u.Username).IsEqualTo("testing").Satisfied());
        }
        
        [Test]
        public void Test_Fluent_Validation_Equal_When_InValid_Returns_Message()
        {
            var user = new User {
                Username = "testingx"
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.AreEqual("Username should be equal to testing", fluentValidation
                          .Check(u => u.Username).IsEqualTo("testing")
                          .Validate().First().Condition.Message);
        }

        [Test]
        public void Test_Fluent_Validation_Equal_Expression()
        {
            var user = new User {
                Password = "password123",
                ConfirmPassword = "password123"
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                          .Check(u => u.Password).IsEqualTo(u=>u.ConfirmPassword).Satisfied());
        }
        
        [Test]
        public void Test_Fluent_Validation_Equal_Expression_When_InValid()
        {
            var user = new User {
                Password = "password123",
                ConfirmPassword = "password1234"
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                           .Check(u => u.Password).IsEqualTo(u => u.ConfirmPassword)
                          .Satisfied());
        }
        
        [Test]
        public void Test_Fluent_Validation_Equal_Expression_When_InValid_Returns_Message()
        {
            var user = new User {
                Password = "password123",
                ConfirmPassword = "password1234"
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.AreEqual("Password should be equal to ConfirmPassword", fluentValidation
                          .Check(u => u.Password).IsEqualTo(u => u.ConfirmPassword)
                          .Validate().First().Condition.Message);
        }

        [Test]
        public void Test_Fluent_Validation_Not_Equal()
        {
            var user = new User {
                Username = "testingx"
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                           .Check(u => u.Username).IsNotEqualTo("testing").Satisfied());
        }
        
        [Test]
        public void Test_Fluent_Validation_Not_Equal_When_Null()
        {
            var user = new User {
                Username = null
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                           .Check(u => u.Username).IsNotEqualTo("testing").Satisfied());
        }
        
        [Test]
        public void Test_Fluent_Validation_NotEqual_When_InValid()
        {
            var user = new User {
                Username = "testing"
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                          .Check(u => u.Username).IsNotEqualTo("testing").Satisfied());
        }
        
        [Test]
        public void Test_Fluent_Validation_NotEqual_When_InValid_Returns_Message()
        {
            var user = new User {
                Username = "testing"
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.AreEqual("Username should not be equal to testing", fluentValidation
                          .Check(u => u.Username).IsNotEqualTo("testing")
                          .Validate().First().Condition.Message);
        }

        [Test]
        public void Test_Fluent_Validation_NotEqual_Expression()
        {
            var user = new User {
                Password = "password123",
                ConfirmPassword = "password1234"
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                          .Check(u => u.Password).IsNotEqualTo(u => u.ConfirmPassword).Satisfied());
        }
        
        [Test]
        public void Test_Fluent_Validation_NotEqual_Expression_When_InValid()
        {
            var user = new User {
                Password = "password1234",
                ConfirmPassword = "password1234"
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                           .Check(u => u.Password).IsNotEqualTo(u => u.ConfirmPassword)
                          .Satisfied());
        }
        
        [Test]
        public void Test_Fluent_Validation_NotEqual_Expression_When_InValid_Returns_Message()
        {
            var user = new User {
                Password = "password1234",
                ConfirmPassword = "password1234"
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.AreEqual("Password should not be equal to ConfirmPassword", fluentValidation
                          .Check(u => u.Password).IsNotEqualTo(u => u.ConfirmPassword)
                          .Validate().First().Condition.Message);
        }

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
