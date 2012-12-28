using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MFlow.Core.Tests.VmlConfiguration
{
    [TestClass]
    public class VmlConfiguration
    {
        [TestMethod]
        public void Test_Fluent_Validation_NotEmpty_False_Loaded_From_Vml()
        {
            var user = new User() { };
            IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "NotEmpty.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_NotEmpty_True_Loaded_From_Vml()
        {
            var user = new User() { Username = "testing" };
            IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "NotEmpty.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        //[TestMethod]
        //public void Test_Fluent_Validation_Contains_False_Loaded_From_Xml()
        //{
        //    var user = new User() { };
        //    IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "Contains.validation.xml");
        //    Assert.IsFalse(fluentValidation.Satisfied());
        //}

        //[TestMethod]
        //public void Test_Fluent_Validation_Contains_True_Loaded_From_Xml()
        //{
        //    var user = new User() { Username = "test admin test" };
        //    IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "Contains.validation.xml");
        //    Assert.IsTrue(fluentValidation.Satisfied());
        //}

        //[TestMethod]
        //public void Test_Fluent_Validation_RegEx_False_Loaded_From_Xml()
        //{
        //    var user = new User() { Username="testing" };
        //    IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "RegEx.validation.xml");
        //    Assert.IsFalse(fluentValidation.Satisfied());
        //}

        //[TestMethod]
        //public void Test_Fluent_Validation_RegEx_True_Loaded_From_Xml()
        //{
        //    var user = new User() { Username="user@somedomain.com" };
        //    IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "RegEx.validation.xml");
        //    Assert.IsTrue(fluentValidation.Satisfied());
        //}

        //[TestMethod]
        //public void Test_Fluent_Validation_IsEmail_False_Loaded_From_Xml()
        //{
        //    var user = new User() { Username = "testing" };
        //    IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsEmail.validation.xml");
        //    Assert.IsFalse(fluentValidation.Satisfied());
        //}

        //[TestMethod]
        //public void Test_Fluent_Validation_IsEmail_True_Loaded_From_Xml()
        //{
        //    var user = new User() { Username = "user@somedomain.com" };
        //    IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsEmail.validation.xml");
        //    Assert.IsTrue(fluentValidation.Satisfied());
        //}

        //[TestMethod]
        //public void Test_Fluent_Validation_Equal_False_Loaded_From_Xml()
        //{
        //    var user = new User() { Username = "testing" };
        //    IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "Equal.validation.xml");
        //    Assert.IsFalse(fluentValidation.Satisfied());
        //}

        //[TestMethod]
        //public void Test_Fluent_Validation_EqualExpression_False_Loaded_From_Xml()
        //{
        //    var user = new User() { Password="123", ConfirmPassword="456" };
        //    IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "EqualExpression.validation.xml");
        //    Assert.IsFalse(fluentValidation.Satisfied());
        //}

        //[TestMethod]
        //public void Test_Fluent_Validation_EqualExpression_True_Loaded_From_Xml()
        //{
        //    var user = new User() { Password = "123", ConfirmPassword = "123" };
        //    IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "EqualExpression.validation.xml");
        //    Assert.IsTrue(fluentValidation.Satisfied());
        //}

        //[TestMethod]
        //public void Test_Fluent_Validation_NotEqualExpression_False_Loaded_From_Xml()
        //{
        //    var user = new User() { Password = "123", ConfirmPassword = "123" };
        //    IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "NotEqualExpression.validation.xml");
        //    Assert.IsFalse(fluentValidation.Satisfied());
        //}

        //[TestMethod]
        //public void Test_Fluent_Validation_NotEqualExpression_True_Loaded_From_Xml()
        //{
        //    var user = new User() { Password = "123", ConfirmPassword = "456" };
        //    IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "NotEqualExpression.validation.xml");
        //    Assert.IsTrue(fluentValidation.Satisfied());
        //}

        [TestMethod]
        public void Test_Fluent_Validation_Equal_True_Loaded_From_Xml()
        {
            var user = new User() { Username = "fred" };
            IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "Equal.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_NotEqual_False_Loaded_From_Vml()
        {
            var user = new User() { Username = "fred" };
            IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "NotEqual.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_NotEqual_True_Loaded_From_Vml()
        {
            var user = new User() { Username = "testing" };
            IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "NotEqual.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        //[TestMethod]
        //public void Test_Fluent_Validation_LessThan_False_Loaded_From_Xml()
        //{
        //    var user = new User() { LoginCount = 12 };
        //    IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "LessThan.validation.xml");
        //    Assert.IsFalse(fluentValidation.Satisfied());
        //}

        //[TestMethod]
        //public void Test_Fluent_Validation_LessThan_True_Loaded_From_Xml()
        //{
        //    var user = new User() { LoginCount = 9};
        //    IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "LessThan.validation.xml");
        //    Assert.IsTrue(fluentValidation.Satisfied());
        //}

        //[TestMethod]
        //public void Test_Fluent_Validation_GreaterThan_False_Loaded_From_Xml()
        //{
        //    var user = new User() { LoginCount = 1 };
        //    IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "GreaterThan.validation.xml");
        //    Assert.IsFalse(fluentValidation.Satisfied());
        //}

        //[TestMethod]
        //public void Test_Fluent_Validation_GreaterThan_True_Loaded_From_Xml()
        //{
        //    var user = new User() { LoginCount = 15 };
        //    IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "GreaterThan.validation.xml");
        //    Assert.IsTrue(fluentValidation.Satisfied());
        //}

        //[TestMethod]
        //public void Test_Fluent_Validation_LessThanOrEqualTo_False_Loaded_From_Xml()
        //{
        //    var user = new User() { LoginCount = 11 };
        //    IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "LessThanOrEqualTo.validation.xml");
        //    Assert.IsFalse(fluentValidation.Satisfied());
        //}

        //[TestMethod]
        //public void Test_Fluent_Validation_LessThanOrEqualTo_True_Loaded_From_Xml()
        //{
        //    var user = new User() { LoginCount = 10 };
        //    IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "LessThanOrEqualTo.validation.xml");
        //    Assert.IsTrue(fluentValidation.Satisfied());
        //}

        //[TestMethod]
        //public void Test_Fluent_Validation_GreaterThanOrEqualTo_False_Loaded_From_Xml()
        //{
        //    var user = new User() { LoginCount = 1 };
        //    IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "GreaterThanOrEqualTo.validation.xml");
        //    Assert.IsFalse(fluentValidation.Satisfied());
        //}

        //[TestMethod]
        //public void Test_Fluent_Validation_GreaterThanOrEqualTo_True_Loaded_From_Xml()
        //{
        //    var user = new User() { LoginCount = 10 };
        //    IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "GreaterThanOrEqualTo.validation.xml");
        //    Assert.IsTrue(fluentValidation.Satisfied());
        //}

        //[TestMethod]
        //public void Test_Fluent_Validation_CustomRule_False_Loaded_From_Xml()
        //{
        //    var user = new User() { LoginCount = 1 };
        //    IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "CustomRule.validation.xml");
        //    Assert.IsFalse(fluentValidation.Satisfied());
        //}


        //[TestMethod]
        //public void Test_Fluent_Validation_CustomRule_True_Loaded_From_Xml()
        //{
        //    var user = new User() { LoginCount = 999 };
        //    IFluentValidation<User> fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "CustomRule.validation.xml");
        //    Assert.IsTrue(fluentValidation.Satisfied());
        //}
    }
}
