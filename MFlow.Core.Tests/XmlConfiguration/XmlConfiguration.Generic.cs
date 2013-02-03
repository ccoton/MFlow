using MFlow.Core.Validation.Factories;
using MFlow.Core.Tests.Supporting;
using NUnit.Framework;

namespace MFlow.Core.Tests.XmlConfiguration
{
    [TestFixture]
    public partial class XmlConfiguration
    {
        [Test]
        public void Test_Fluent_Validation_IsRequired_False_Loaded_From_Xml()
        {
            var user = new User();
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsRequired.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsRequired_True_Loaded_From_Xml()
        {
            var user = new User {
                Username = "testing"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsRequired.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }
        
        [Test]
        public void Test_Fluent_Validation_Equal_False_Loaded_From_Xml()
        {
            var user = new User {
                Username = "testing"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "Equal.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_EqualExpression_False_Loaded_From_Xml()
        {
            var user = new User {
                Password = "123",
                ConfirmPassword = "456"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "EqualExpression.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_EqualExpression_True_Loaded_From_Xml()
        {
            var user = new User {
                Password = "123",
                ConfirmPassword = "123"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "EqualExpression.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_NotEqualExpression_False_Loaded_From_Xml()
        {
            var user = new User {
                Password = "123",
                ConfirmPassword = "123"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "NotEqualExpression.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_NotEqualExpression_True_Loaded_From_Xml()
        {
            var user = new User {
                Password = "123",
                ConfirmPassword = "456"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "NotEqualExpression.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_Equal_True_Loaded_From_Xml()
        {
            var user = new User {
                Username = "fred"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "Equal.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_NotEqual_False_Loaded_From_Xml()
        {
            var user = new User {
                Username = "fred"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "NotEqual.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_NotEqual_True_Loaded_From_Xml()
        {
            var user = new User {
                Username = "testing"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "NotEqual.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }
    }
}
