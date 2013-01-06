using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using NUnit.Framework;

namespace MFlow.Core.Tests.XmlConfiguration
{
    [TestFixture]
    public class XmlConfiguration
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
        public void Test_Fluent_Validation_IsRequired_False_Loaded_From_Xml()
        {
            var user = new User() { };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsRequired.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsRequired_True_Loaded_From_Xml()
        {
            var user = new User() { Username = "testing" };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsRequired.validation.xml");
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

        [Test]
        public void Test_Fluent_Validation_Equal_False_Loaded_From_Xml()
        {
            var user = new User() { Username = "testing" };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "Equal.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_EqualExpression_False_Loaded_From_Xml()
        {
            var user = new User() { Password="123", ConfirmPassword="456" };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "EqualExpression.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_EqualExpression_True_Loaded_From_Xml()
        {
            var user = new User() { Password = "123", ConfirmPassword = "123" };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "EqualExpression.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_NotEqualExpression_False_Loaded_From_Xml()
        {
            var user = new User() { Password = "123", ConfirmPassword = "123" };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "NotEqualExpression.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_NotEqualExpression_True_Loaded_From_Xml()
        {
            var user = new User() { Password = "123", ConfirmPassword = "456" };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "NotEqualExpression.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_Equal_True_Loaded_From_Xml()
        {
            var user = new User() { Username = "fred" };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "Equal.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_NotEqual_False_Loaded_From_Xml()
        {
            var user = new User() { Username = "fred" };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "NotEqual.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_NotEqual_True_Loaded_From_Xml()
        {
            var user = new User() { Username = "testing" };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "NotEqual.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_After_False_Loaded_From_Xml()
        {
            var user = new User() { LastLogin = DateTime.Parse("01/02/2010") };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "After.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_After_True_Loaded_From_Xml()
        {
            var user = new User() { LastLogin = DateTime.Parse("01/02/2020") };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "After.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_Before_False_Loaded_From_Xml()
        {
            var user = new User() { LastLogin = DateTime.Parse("01/02/2020") };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "Before.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_Before_True_Loaded_From_Xml()
        {
            var user = new User() { LastLogin = DateTime.Parse("01/02/2010") };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "Before.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_On_False_Loaded_From_Xml()
        {
            var user = new User() { LastLogin = DateTime.Parse("01/02/2015") };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "On.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_On_True_Loaded_From_Xml()
        {
            var user = new User() { LastLogin = DateTime.Parse("01/01/2015") };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "On.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

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

        [Test]
        public void Test_Fluent_Validation_IsThisYear_False_Loaded_From_Xml()
        {
            var user = new User() { LastLogin = DateTime.Now.AddYears(1) };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsThisYear.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsThisYear_True_Loaded_From_Xml()
        {
            var user = new User() { LastLogin = DateTime.Now };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsThisYear.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsThisMonth_False_Loaded_From_Xml()
        {
            var user = new User() { LastLogin = DateTime.Now.AddMonths(1) };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsThisMonth.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsThisMonth_True_Loaded_From_Xml()
        {
            var user = new User() { LastLogin = DateTime.Now };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsThisMonth.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsThisWeek_False_Loaded_From_Xml()
        {
            var user = new User() { LastLogin = DateTime.Now.AddMonths(1) };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsThisWeek.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsThisWeek_True_Loaded_From_Xml()
        {
            var user = new User() { LastLogin = DateTime.Now };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsThisWeek.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsToday_False_Loaded_From_Xml()
        {
            var user = new User() { LastLogin = DateTime.Now.AddDays(1) };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsToday.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsToday_True_Loaded_From_Xml()
        {
            var user = new User() { LastLogin = DateTime.Now };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsToday.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_CustomRule_False_Loaded_From_Xml()
        {
            var user = new User() { LoginCount = 1 };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "CustomRule.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }


        [Test]
        public void Test_Fluent_Validation_CustomRule_True_Loaded_From_Xml()
        {
            var user = new User() { LoginCount = 999 };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "CustomRule.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

    }
}
