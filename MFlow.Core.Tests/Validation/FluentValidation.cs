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
        public void Test_Fluent_Validation_Constructor_Exception()
        {
            var fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(null);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_Valid_Expression()
        {
            var user = new User() { Password = "password123", Username = "testing" };
            var fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .If(u => u.Username == "testing").Satisfied());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_InValid_Expression()
        {
            var user = new User() { Password = "password123", Username = "testing" };
            var fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .If(u => u.Username == "xxx").Satisfied());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_Valid_Expression_Chain()
        {
            var user = new User() { Password = "password123", Username = "testing" };
            var fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .If(u => u.Username == "testing")
                .And(u => u.Password == "password123").Satisfied());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_InValid_Expression_Chain()
        {
            var user = new User() { Password = "password123", Username = "testingx" };
            var fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .If(u => u.Username == "testing")
                .And(u => u.Password == "password123").Satisfied());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_Valid_Or_Expression_Chain()
        {
            var user = new User() { Password = "password1213", Username = "testingx" };
            var fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .If(u => u.Username == "testing")
                .And(u => u.Password == "password123")
                .Or(u => u.Username == "testingx").Satisfied());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_InValid_Or_Expression_Chain()
        {
            var user = new User() { Password = "password1234", Username = "testingx" };
            var fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .If(u => u.Username == "testing")
                .And(u => u.Password == "password123")
                .Or(u => u.Username == "test").Satisfied());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_Valid_Expression_Executes()
        {
            var user = new User() { Password = "password123", Username = "testing" };
            var fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            fluentValidation
                .If(u => u.Username == "testing")
                .And(u => u.Password == "password123")
                .Then(() => {
                    user.Username = "valid";
                });

            Assert.IsTrue(user.Username == "valid");
        }

    }
}
