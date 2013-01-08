using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using NUnit.Framework;

namespace MFlow.Core.Tests.XmlConfiguration
{
    [TestFixture]
    public partial class XmlConfiguration
    {
        [Test]
        public void Test_Fluent_Validation_LessThan_False_Loaded_From_Xml()
        {
            var user = new User() { LoginCount = 12 };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "LessThan.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_LessThan_True_Loaded_From_Xml()
        {
            var user = new User() { LoginCount = 9};
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "LessThan.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_GreaterThan_False_Loaded_From_Xml()
        {
            var user = new User() { LoginCount = 1 };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "GreaterThan.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_GreaterThan_True_Loaded_From_Xml()
        {
            var user = new User() { LoginCount = 15 };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "GreaterThan.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_LessThanOrEqualTo_False_Loaded_From_Xml()
        {
            var user = new User() { LoginCount = 11 };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "LessThanOrEqualTo.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_LessThanOrEqualTo_True_Loaded_From_Xml()
        {
            var user = new User() { LoginCount = 10 };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "LessThanOrEqualTo.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_GreaterThanOrEqualTo_False_Loaded_From_Xml()
        {
            var user = new User() { LoginCount = 1 };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "GreaterThanOrEqualTo.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_GreaterThanOrEqualTo_True_Loaded_From_Xml()
        {
            var user = new User() { LoginCount = 10 };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "GreaterThanOrEqualTo.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

    }
}
