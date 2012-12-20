using System;
using MFlow.Core.Conditions;
using MFlow.Core.Tests.Supporting;
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
            IFluentValidation<object> fluentValidation = new MFlow.Core.Validation.FluentValidation<object>(new object());
            var username = "";
            fluentValidation.If(string.IsNullOrEmpty(username)).Throw(new ArgumentException("Username"));
        }

        [TestMethod]
        public void Test_Simple_Fluent_Validation_Doesnt_Throw_Exception()
        {
            IFluentValidation<object> fluentValidation = new MFlow.Core.Validation.FluentValidation<object>(new object());
            var username = "username";
            fluentValidation.If(string.IsNullOrEmpty(username)).Throw(new ArgumentException("Username"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Chained_Fluent_Validation_Throws_Exception()
        {
            IFluentValidation<object> fluentValidation = new MFlow.Core.Validation.FluentValidation<object>(new object());
            var username = ""; var password = "";
            fluentValidation
                .If(string.IsNullOrEmpty(username))
                .And(string.IsNullOrEmpty(password))
                .Throw(new ArgumentException("Username and Password"));
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Doesnt_Throw_Exception()
        {
            IFluentValidation<object> fluentValidation = new MFlow.Core.Validation.FluentValidation<object>(new object());
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
            IFluentValidation<object> fluentValidation = new MFlow.Core.Validation.FluentValidation<object>(new object());
            var username = "username"; var password = "";
            fluentValidation
                .If(string.IsNullOrEmpty(username))
                .Or(string.IsNullOrEmpty(password))
                .Throw(new ArgumentException("Username or Password"));
        }

        [TestMethod]
        public void Test_Chained_Fluent_Or_Validation_Doesnt_Throw_Exception()
        {
            IFluentValidation<object> fluentValidation = new MFlow.Core.Validation.FluentValidation<object>(new object());
            var username = "test"; var password = "test";
            fluentValidation
                .If(string.IsNullOrEmpty(username))
                .Or(string.IsNullOrEmpty(password))
                .Throw(new ArgumentException("Username or Password"));
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_Valid_Expression()
        {
            var user = new User() { Password = "password123", Username = "testing" };

            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .If(u => u.Username == "testing").Satisfied());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_InValid_Expression()
        {
            var user = new User() { Password = "password123", Username = "testing" };

            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .If(u => u.Username == "xxx").Satisfied());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_Valid_Expression_Chain()
        {
            var user = new User() { Password = "password123", Username = "testing" };

            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .If(u => u.Username == "testing")
                .And(u => u.Password == "password123").Satisfied());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_InValid_Expression_Chain()
        {
            var user = new User() { Password = "password123", Username = "testingx" };

            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .If(u => u.Username == "testing")
                .And(u => u.Password == "password123").Satisfied());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_Valid_Or_Expression_Chain()
        {
            var user = new User() { Password = "password1213", Username = "testingx" };

            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .If(u => u.Username == "testing")
                .And(u => u.Password == "password123")
                .Or(u => u.Username == "testingx").Satisfied());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_InValid_Or_Expression_Chain()
        {
            var user = new User() { Password = "password1234", Username = "testingx" };

            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .If(u => u.Username == "testing")
                .And(u => u.Password == "password123")
                .Or(u => u.Username == "test").Satisfied());
        }

    }
}
