using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using NUnit.Framework;

namespace MFlow.Core.Tests.XmlConfiguration
{
    [TestFixture]
    public partial class XmlConfiguration
    {
        [Test]
        public void Test_Fluent_Validation_NotEmpty_False_Loaded_From_Xml()
        {
            var user = new User() { };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "NotEmpty.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_NotEmpty_True_Loaded_From_Xml()
        {
            var user = new User() { Username = "testing" };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "NotEmpty.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsLength_False_Loaded_From_Xml()
        {
            var user = new User() { };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsLength.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsLength_True_Loaded_From_Xml()
        {
            var user = new User() { Username = "12345678901234567890" };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsLength.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

		[Test]
		public void Test_Fluent_Validation_IsNumeric_False_Loaded_From_Xml()
		{
			var user = new User() { Username = "abc" };
			var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsNumeric.validation.xml");
			Assert.IsFalse(fluentValidation.Satisfied());
		}
		
		[Test]
		public void Test_Fluent_Validation_IsNumeric_True_Loaded_From_Xml()
		{
			var user = new User() { Username = "123456789" };
			var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsNumeric.validation.xml");
			Assert.IsTrue(fluentValidation.Satisfied());
		}

		[Test]
		public void Test_Fluent_Validation_IsAlpha_False_Loaded_From_Xml()
		{
			var user = new User() { Username = "123" };
			var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsAlpha.validation.xml");
			Assert.IsFalse(fluentValidation.Satisfied());
		}
		
		[Test]
		public void Test_Fluent_Validation_IsAlpha_True_Loaded_From_Xml()
		{
			var user = new User() { Username = "abcdefg" };
			var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsAlpha.validation.xml");
			Assert.IsTrue(fluentValidation.Satisfied());
		}

        [Test]
        public void Test_Fluent_Validation_IsLongerThan_False_Loaded_From_Xml()
        {
            var user = new User() {Username = "123" };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsLongerThan.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }
        
        [Test]
        public void Test_Fluent_Validation_IsLongerThan_True_Loaded_From_Xml()
        {
            var user = new User() { Username = "1234567890123456789012" };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsLongerThan.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }
        
        [Test]
        public void Test_Fluent_Validation_IsShorterThan_False_Loaded_From_Xml()
        {
            var user = new User() { Username = "12321323132321321321321" };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsShorterThan.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsShorterThan_True_Loaded_From_Xml()
        {
            var user = new User() { Username = "12321" };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsShorterThan.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsCreditCard_False_Loaded_From_Xml()
        {
            var user = new User() { };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsCreditCard.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsCreditCard_True_Loaded_From_Xml()
        {
            var user = new User() { Username = "5105 1051 0510 5100" };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsCreditCard.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsPostCode_False_Loaded_From_Xml()
        {
            var user = new User() { };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsPostCode.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsPostCode_True_Loaded_From_Xml()
        {
            var user = new User() { Username = "B69 1TE" };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsPostCode.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsZipCode_False_Loaded_From_Xml()
        {
            var user = new User() { };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsZipCode.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsZipCode_True_Loaded_From_Xml()
        {
            var user = new User() { Username = "35801" };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsZipCode.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_Contains_False_Loaded_From_Xml()
        {
            var user = new User() { };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "Contains.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_Contains_True_Loaded_From_Xml()
        {
            var user = new User() { Username = "test admin test" };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "Contains.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_RegEx_False_Loaded_From_Xml()
        {
            var user = new User() { Username="testing" };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "RegEx.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_RegEx_True_Loaded_From_Xml()
        {
            var user = new User() { Username="user@somedomain.com" };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "RegEx.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsEmail_False_Loaded_From_Xml()
        {
            var user = new User() { Username = "testing" };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsEmail.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsEmail_True_Loaded_From_Xml()
        {
            var user = new User() { Username = "user@somedomain.com" };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsEmail.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }
    }
}
