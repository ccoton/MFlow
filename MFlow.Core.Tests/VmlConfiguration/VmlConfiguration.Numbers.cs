using MFlow.Core.Validation.Factories;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using NUnit.Framework;

namespace MFlow.Core.Tests.VmlConfiguration
{
    [TestFixture]
    public partial class VmlConfiguration
    {
        [Test]
        public void Test_Fluent_Validation_LessThan_False_Loaded_From_Vml()
        {
            var user = new User() { LoginCount = 12 };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "LessThan.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_LessThan_True_Loaded_From_Vml()
        {
            var user = new User() { LoginCount = 9 };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "LessThan.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_GreaterThan_False_Loaded_From_Vml()
        {
            var user = new User() { LoginCount = 1 };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "GreaterThan.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_GreaterThan_True_Loaded_From_Vml()
        {
            var user = new User() { LoginCount = 15 };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "GreaterThan.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_LessThanOrEqualTo_False_Loaded_From_Vml()
        {
            var user = new User() { LoginCount = 11 };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "LessThanOrEqualTo.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_LessThanOrEqualTo_True_Loaded_From_Vml()
        {
            var user = new User() { LoginCount = 10 };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "LessThanOrEqualTo.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_GreaterThanOrEqualTo_False_Loaded_From_Vml()
        {
            var user = new User() { LoginCount = 1 };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "GreaterThanOrEqualTo.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_GreaterThanOrEqualTo_True_Loaded_From_Vml()
        {
            var user = new User() { LoginCount = 10 };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "GreaterThanOrEqualTo.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }
    }
}
