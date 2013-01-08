using System;
using MFlow.Core.Validation;
using NUnit.Framework;

namespace MFlow.Core.Tests.Validation
{
    [TestFixture]
    public class GenericValidator
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Simple_Fluent_Validation_Throws_Exception()
        {
            var fluentValidation = new MFlow.Core.Validation.GenericValidator();
            var username = "";
            fluentValidation.If(string.IsNullOrEmpty(username)).Throw(new ArgumentException("Username"));
        }

        [Test]
        public void Test_Simple_Fluent_Validation_Doesnt_Throw_Exception()
        {
            var fluentValidation = new MFlow.Core.Validation.GenericValidator();
            var username = "username";
            fluentValidation.If(string.IsNullOrEmpty(username)).Throw(new ArgumentException("Username"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Chained_Fluent_Validation_Throws_Exception()
        {
            var fluentValidation = new MFlow.Core.Validation.GenericValidator();
            var username = ""; var password = "";
            fluentValidation
                .If(string.IsNullOrEmpty(username))
                .And(string.IsNullOrEmpty(password))
                .Throw(new ArgumentException("Username and Password"));
        }

        [Test]
        public void Test_Chained_Fluent_Validation_Doesnt_Throw_Exception()
        {
            var fluentValidation = new MFlow.Core.Validation.GenericValidator();
            var username = "username"; var password = "password";
            fluentValidation
                .If(string.IsNullOrEmpty(username))
                .And(string.IsNullOrEmpty(password))
                .Throw(new ArgumentException("Username and Password"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Chained_Fluent_Or_Validation_Throws_Exception()
        {
            var fluentValidation = new MFlow.Core.Validation.GenericValidator();
            var username = "username"; var password = "";
            fluentValidation
                .If(string.IsNullOrEmpty(username))
                .Or(string.IsNullOrEmpty(password))
                .Throw(new ArgumentException("Username or Password"));
        }

        [Test]
        public void Test_Chained_Fluent_Or_Validation_Doesnt_Throw_Exception()
        {
            var fluentValidation = new MFlow.Core.Validation.GenericValidator();
            var username = "test"; var password = "test";
            fluentValidation
                .If(string.IsNullOrEmpty(username))
                .Or(string.IsNullOrEmpty(password))
                .Throw(new ArgumentException("Username or Password"));
        }
    }
}
