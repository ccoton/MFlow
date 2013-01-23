using System;
using MFlow.Core.Validation.Factories;
using MFlow.Core.Tests.Supporting;
using NUnit.Framework;

namespace MFlow.Core.Tests.VmlConfiguration
{
    [TestFixture]
    public partial class VmlConfiguration
    {
        [Test]
        public void Test_Fluent_Validation_IsRequired_False_Loaded_From_Vml()
        {
            var user = new User();
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsRequired.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsRequired_True_Loaded_From_Vml()
        {
            var user = new User {
                Username = "testing"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsRequired.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_Equal_False_Loaded_From_Vml()
        {
            var user = new User {
                Username = "testing"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "Equal.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_EqualExpression_False_Loaded_From_Vml()
        {
            var user = new User {
                Password = "123",
                ConfirmPassword = "456"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "EqualExpression.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_EqualExpression_True_Loaded_From_Vml()
        {
            var user = new User {
                Password = "123",
                ConfirmPassword = "123"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "EqualExpression.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_NotEqualExpression_False_Loaded_From_Vml()
        {
            var user = new User {
                Password = "123",
                ConfirmPassword = "123"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "NotEqualExpression.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_NotEqualExpression_True_Loaded_From_Vml()
        {
            var user = new User {
                Password = "123",
                ConfirmPassword = "456"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "NotEqualExpression.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_Equal_True_Loaded_From_Xml()
        {
            var user = new User {
                Username = "fred"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "Equal.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_NotEqual_False_Loaded_From_Vml()
        {
            var user = new User {
                Username = "fred"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "NotEqual.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_NotEqual_True_Loaded_From_Vml()
        {
            var user = new User {
                Username = "testing"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "NotEqual.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }
    }
}
