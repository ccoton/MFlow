using System;
using MFlow.Core.Conditions;
using MFlow.Core.Events;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;

namespace MFlow.Core.Tests.Validation
{
    [TestClass]
    public class FluentValidation
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Fluent_Validation_Constructor_Exception()
        {
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(null);
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

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_Valid_Expression_Executes()
        {
            var user = new User() { Password = "password123", Username = "testing" };
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            fluentValidation
                .If(u => u.Username == "testing")
                .And(u => u.Password == "password123")
                .Then(() =>
                {
                    user.Username = "valid";
                });

            Assert.IsTrue(user.Username == "valid");
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_Valid_Expression_Executes_Else()
        {
            var user = new User() { Password = "password123", Username = "testing2" };
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            fluentValidation
                .If(u => u.Username == "testing")
                .And(u => u.Password == "password123")
                .Then(() => { user.Username = "valid"; })
                .Else(() => { user.Username = "invalid"; });

            Assert.IsTrue(user.Username == "invalid");
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Raises_Event()
        {
            var user = new User() { Password = "password123", Username = "testing" };
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);

            var events = new EventsFactory().GetEventStore();
            events.Register<UserCreatedEvent>(s =>
                {
                    s.Source.Username = "caught event";
                    user = s.Source;
                });

            fluentValidation
                .If(u => u.Username == "testing")
                .And(u => u.Password == "password123")
                .Raise(new UserCreatedEvent(user));

            Assert.AreEqual(user.Username, "caught event");
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Returns_Correct_Number_Of_Results()
        {
            var user = new User() { Password = "password123", Username = "testingx" };
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            var results = fluentValidation
                .If(u => u.Username == "testing")
                .And(u => u.Password == "password123").Validate();

            Assert.AreEqual(1, results.ToList().Count());

        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Returns_Correct_Key()
        {
            var user = new User() { Password = "password123", Username = "testingx" };
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            var results = fluentValidation
                .If(u => u.Username == "testing")
                .And(u => u.Password == "password123").Validate();

            Assert.AreEqual("Username", results.First().Condition.Key);

        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Returns_Correct_Message()
        {
            var user = new User() { Password = "password123", Username = "testingx" };
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            var results = fluentValidation
                .If(u => u.Username == "testing", message:"Username is not valid")
                .And(u => u.Password == "password123").Validate();

            Assert.AreEqual("Username is not valid", results.First().Condition.Message);

        }


        [TestMethod]
        public void Test_Chained_Fluent_Validation_Returns_Correct_Number_Of_Multipe_Results()
        {
            var user = new User() { Password = "password1234", Username = "testingx" };
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            var results = fluentValidation
                .If(u => u.Username == "testing")
                .And(u => u.Password == "password123").Validate();

            Assert.AreEqual(2, results.ToList().Count());

        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Returns_Correct_Keys()
        {
            var user = new User() { Password = "password123", Username = "testingx" };
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            var results = fluentValidation
                .If(u => u.Username == "testing")
                .And(u => u.Password == "password1234").Validate();

            Assert.AreEqual("Username", results.First().Condition.Key);
            Assert.AreEqual("Password", results.Skip(1).Take(1).First().Condition.Key);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Returns_Correct_Messages()
        {
            var user = new User() { Password = "password1234", Username = "testingx" };
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            var results = fluentValidation
                .If(u => u.Username == "testing", message: "Username is not valid")
                .And(u => u.Password == "password123", message:"Password is now valid").Validate();

            Assert.AreEqual("Username is not valid", results.First().Condition.Message);
            Assert.AreEqual("Password is now valid", results.Skip(1).Take(1).First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Returns_Correct_Complex_Keys()
        {
            IFluentValidation<Thread> fluentValidation = new MFlow.Core.Validation.FluentValidation<Thread>(Thread.CurrentThread);
            var results = fluentValidation
                .If(u => u.CurrentCulture.DisplayName == "")
                .And(u => u.CurrentCulture.EnglishName == "").Validate();

            Assert.AreEqual("CurrentCulture.DisplayName", results.First().Condition.Key);
            Assert.AreEqual("CurrentCulture.EnglishName", results.Skip(1).Take(1).First().Condition.Key);
        }
    }
}
