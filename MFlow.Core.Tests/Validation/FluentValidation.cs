using System;
using MFlow.Core.Conditions;
using MFlow.Core.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MFlow.Core.Tests.Validation
{
    [TestClass]
    public class FluentValidation
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Simple_Fluent_Validation_Throws_Exception()
        {
            IFluentValidation fluentValidation = new MFlow.Core.Validation.FluentValidation();
            var username = "";
            fluentValidation.If(string.IsNullOrEmpty(username)).Throw(new ArgumentException("Username"));
        }

        [TestMethod]
        public void Test_Simple_Fluent_Validation_Doesnt_Throw_Exception()
        {
            IFluentValidation fluentValidation = new MFlow.Core.Validation.FluentValidation();
            var username = "username";
            fluentValidation.If(string.IsNullOrEmpty(username)).Throw(new ArgumentException("Username"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Chained_Fluent_Validation_Throws_Exception()
        {
            IFluentValidation fluentValidation = new MFlow.Core.Validation.FluentValidation();
            var username = ""; var password = "";
            fluentValidation
                .If(string.IsNullOrEmpty(username))
                .And(string.IsNullOrEmpty(password))
                .Throw(new ArgumentException("Username and Password"));
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Doesnt_Throw_Exception()
        {
            IFluentValidation fluentValidation = new MFlow.Core.Validation.FluentValidation();
            var username = "username"; var password = "password";
            fluentValidation
                .If(string.IsNullOrEmpty(username))
                .And(string.IsNullOrEmpty(password))
                .Throw(new ArgumentException("Username and Password"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Chained_Fluent_Or_Validation_Throws_Exception()
        {
            IFluentValidation fluentValidation = new MFlow.Core.Validation.FluentValidation();
            var username = "username"; var password = "";
            fluentValidation
                .If(string.IsNullOrEmpty(username))
                .Or(string.IsNullOrEmpty(password))
                .Throw(new ArgumentException("Username or Password"));
        }

        [TestMethod]
        public void Test_Chained_Fluent_Or_Validation_Doesnt_Throw_Exception()
        {
            IFluentValidation fluentValidation = new MFlow.Core.Validation.FluentValidation();
            var username = "test"; var password = "test";
            fluentValidation
                .If(string.IsNullOrEmpty(username))
                .Or(string.IsNullOrEmpty(password))
                .Throw(new ArgumentException("Username or Password"));
        }

    }
}
