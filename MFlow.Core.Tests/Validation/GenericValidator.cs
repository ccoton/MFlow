using System;
using MFlow.Core.Conditions;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MFlow.Core.Tests.Validation
{
    [TestClass]
    public class GenericValidator
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Simple_Fluent_Validation_Throws_Exception()
        {
            var fluentValidation = new MFlow.Core.Validation.GenericValidator();
            var username = "";
            fluentValidation.Check(string.IsNullOrEmpty(username)).Throw(new ArgumentException("Username"));
        }

        [TestMethod]
        public void Test_Simple_Fluent_Validation_Doesnt_Throw_Exception()
        {
            var fluentValidation = new MFlow.Core.Validation.GenericValidator();
            var username = "username";
            fluentValidation.Check(string.IsNullOrEmpty(username)).Throw(new ArgumentException("Username"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Chained_Fluent_Validation_Throws_Exception()
        {
            var fluentValidation = new MFlow.Core.Validation.GenericValidator();
            var username = ""; var password = "";
            fluentValidation
                .Check(string.IsNullOrEmpty(username))
                .Check(string.IsNullOrEmpty(password))
                .Throw(new ArgumentException("Username and Password"));
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Doesnt_Throw_Exception()
        {
            var fluentValidation = new MFlow.Core.Validation.GenericValidator();
            var username = "username"; var password = "password";
            fluentValidation
                .Check(string.IsNullOrEmpty(username))
                .Check(string.IsNullOrEmpty(password))
                .Throw(new ArgumentException("Username and Password"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Chained_Fluent_Or_Validation_Throws_Exception()
        {
            var fluentValidation = new MFlow.Core.Validation.GenericValidator();
            var username = "username"; var password = "";
            fluentValidation
                .Check(string.IsNullOrEmpty(username))
                .Or(string.IsNullOrEmpty(password))
                .Throw(new ArgumentException("Username or Password"));
        }

        [TestMethod]
        public void Test_Chained_Fluent_Or_Validation_Doesnt_Throw_Exception()
        {
            var fluentValidation = new MFlow.Core.Validation.GenericValidator();
            var username = "test"; var password = "test";
            fluentValidation
                .Check(string.IsNullOrEmpty(username))
                .Or(string.IsNullOrEmpty(password))
                .Throw(new ArgumentException("Username or Password"));
        }
    }
}
