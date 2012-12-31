using System;
using MFlow.Core.Conditions;
using MFlow.Core.Events;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;
using System.Globalization;

namespace MFlow.Core.Tests.Validation
{
    [TestClass]
    public partial class FluentValidation
    {

        private readonly IFluentValidationFactory _factory = new FluentValidationFactory();

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Fluent_Validation_Constructor_Exception()
        {
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(null);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_Valid_Expression()
        {
            var user = new User() { Password = "password123", Username = "testing" };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Equal(u => u.Username, "testing").Satisfied());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_InValid_Expression()
        {
            var user = new User() { Password = "password123", Username = "testing" };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Equal(u => u.Username, "xxx").Satisfied());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_Valid_Expression_Chain()
        {
            var user = new User() { Password = "password123", Username = "testing" };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Equal(u => u.Username, "testing")
                .Equal(u => u.Password, "password123").Satisfied());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_InValid_Expression_Chain()
        {
            var user = new User() { Password = "password123", Username = "testingx" };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Equal(u => u.Username, "testing")
                .Equal(u => u.Password, "password123").Satisfied());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_Valid_Or_Expression_Chain()
        {
            var user = new User() { Password = "password1213", Username = "testingx" };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Equal(u => u.Username, "testing")
                .Equal(u => u.Password, "password123")
                .Equal(u => u.Username, "testingx", conditionType: ConditionType.Or).Satisfied());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_InValid_Or_Expression_Chain()
        {
            var user = new User() { Password = "password1234", Username = "testingx" };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Equal(u => u.Username, "testing")
                .Equal(u => u.Password, "password123")
                .Equal(u => u.Username, "test", conditionType: ConditionType.Or).Satisfied());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_Valid_Expression_Executes()
        {
            var user = new User() { Password = "password123", Username = "testing" };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            fluentValidation
                .Equal(u => u.Username, "testing")
                .Equal(u => u.Password, "password123")
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
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            fluentValidation
                .Equal(u => u.Username, "testing")
                .Equal(u => u.Password, "password123")
                .Then(() => { user.Username = "valid"; })
                .Else(() => { user.Username = "invalid"; });

            Assert.IsTrue(user.Username == "invalid");
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Raises_Event()
        {
            var user = new User() { Password = "password123", Username = "testing" };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);

            var events = new EventsFactory().GetEventStore();
            events.Register<UserCreatedEvent>(s =>
                {
                    s.Source.Username = "caught event";
                    user = s.Source;
                });

            fluentValidation
                .Equal(u => u.Username, "testing")
                .Equal(u => u.Password, "password123")
                .Raise(new UserCreatedEvent(user));

            Assert.AreEqual(user.Username, "caught event");
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Returns_Correct_Number_Of_Results()
        {
            var user = new User() { Password = "password123", Username = "testingx" };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Equal(u => u.Username, "testing")
                .Equal(u => u.Password, "password123").Validate();

            Assert.AreEqual(1, results.ToList().Count());

        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Returns_Correct_Key()
        {
            var user = new User() { Password = "password123", Username = "testingx" };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Equal(u => u.Username, "testing")
                .Equal(u => u.Password, "password123").Validate();

            Assert.AreEqual("Username", results.First().Condition.Key);

        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Returns_Correct_Message()
        {
            var user = new User() { Password = "password123", Username = "testingx" };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Equal(u => u.Username, "testing", message: "Username is not valid")
                .Equal(u => u.Password, "password123").Validate();

            Assert.AreEqual("Username is not valid", results.First().Condition.Message);

        }


        [TestMethod]
        public void Test_Chained_Fluent_Validation_Returns_Correct_Number_Of_Multipe_Results()
        {
            var user = new User() { Password = "password1234", Username = "testingx" };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Equal(u => u.Username, "testing")
                .Equal(u => u.Password, "password123").Validate();

            Assert.AreEqual(2, results.ToList().Count());

        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Returns_Correct_Keys()
        {
            var user = new User() { Password = "password123", Username = "testingx" };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Equal(u => u.Username, "testing")
                .Equal(u => u.Password, "password1234").Validate();

            Assert.AreEqual("Username", results.First().Condition.Key);
            Assert.AreEqual("Password", results.Skip(1).Take(1).First().Condition.Key);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Returns_Correct_Messages()
        {
            var user = new User() { Password = "password1234", Username = "testingx" };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Equal(u => u.Username, "testing", message: "Username is not valid")
                .Equal(u => u.Password, "password123", message: "Password is now valid").Validate();

            Assert.AreEqual("Username is not valid", results.First().Condition.Message);
            Assert.AreEqual("Password is now valid", results.Skip(1).Take(1).First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Returns_Correct_Complex_Keys()
        {
            IFluentValidation<Thread> fluentValidation = _factory.GetFluentValidation<Thread>(Thread.CurrentThread);
            var results = fluentValidation
                .Equal(u => u.CurrentCulture.DisplayName, "")
                .Equal(u => u.CurrentCulture.EnglishName, "").Validate();

            Assert.AreEqual("CurrentCulture.DisplayName", results.First().Condition.Key);
            Assert.AreEqual("CurrentCulture.EnglishName", results.Skip(1).Take(1).First().Condition.Key);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_NotNullOrEmpty_With_Valid_Value()
        {
            var user = new User() { Password = "password1234", Username = "testingx" };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .NotEmpty(u => u.Username, message: "Username is not valid")
                .Validate();

            Assert.AreEqual(0, results.Count());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_NotNullOrEmpty_Message_Lookup()
        {
            var user = new User() { Password = "password1234", Username = "" };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .NotEmpty(u => u.Username)
                .Validate();

            Assert.AreEqual("Username should not be empty", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Equal_Message_Lookup()
        {
            var user = new User() { Password = "password1234", Username = "" };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Equal(u => u.Username, "test")
                .Validate();

            Assert.AreEqual("Username should be equal to test", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_NotEqual_Message_Lookup()
        {
            var user = new User() { Password = "password1234", Username = "test" };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .NotEqual(u => u.Username, "test")
                .Validate();

            Assert.AreEqual("Username should not be equal to test", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_LessThan_Message_Lookup()
        {
            var user = new User() { LoginCount = 11 };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .LessThan(u => u.LoginCount, 10)
                .Validate();

            Assert.AreEqual("LoginCount should be less than 10", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_LessThanOrEqualTo_Message_Lookup()
        {
            var user = new User() { LoginCount = 11 };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .LessThanOrEqualTo(u => u.LoginCount, 10)
                .Validate();

            Assert.AreEqual("LoginCount should be less than or equal to 10", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_GreaterThan_Message_Lookup()
        {
            var user = new User() { LoginCount = 9 };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .GreaterThan(u => u.LoginCount, 10)
                .Validate();

            Assert.AreEqual("LoginCount should be greater than 10", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_GreaterThanOrEqualTo_Message_Lookup()
        {
            var user = new User() { LoginCount = 9 };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .GreaterThanOrEqualTo(u => u.LoginCount, 10)
                .Validate();

            Assert.AreEqual("LoginCount should be greater than or equal to 10", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Email_Message_Lookup()
        {
            var user = new User() { Username = "fred" };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .IsEmail(u => u.Username)
                .Validate();

            Assert.AreEqual("Username should be an email", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Contains_Message_Lookup()
        {
            var user = new User() { Username = "fred" };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Contains(u => u.Username, "dave")
                .Validate();

            Assert.AreEqual("Username should contain dave", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Before_Message_Lookup()
        {
            var user = new User() { LastLogin = DateTime.Now };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Before(u => u.LastLogin, DateTime.Parse("01/01/2001 00:00:00"))
                .Validate();

            Assert.AreEqual("LastLogin should be before 01/01/2001 00:00:00", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_After_Message_Lookup()
        {
            var user = new User() { LastLogin = DateTime.Parse("01/01/2001 00:00:00") };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .After(u => u.LastLogin, DateTime.Parse("01/01/2012 00:00:00"))
                .Validate();

            Assert.AreEqual("LastLogin should be after 01/01/2012 00:00:00", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_On_Message_Lookup()
        {
            var user = new User() { LastLogin = DateTime.Parse("01/01/2001 00:00:00") };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .On(u => u.LastLogin, DateTime.Parse("01/01/2012 00:00:00"))
                .Validate();

            Assert.AreEqual("LastLogin should be on 01/01/2012 00:00:00", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_On_Message_Lookup_Different_Culture()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");

            var user = new User() { LastLogin = DateTime.Parse("01/01/2001 00:00:00") };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .On(u => u.LastLogin, DateTime.Parse("01/01/2012 00:00:00"))
                .Validate();

            Assert.AreEqual("LastLogin doit être mis sur 01/01/2012 00:00:00", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Message_Lookup_Missing_Culture()
        {

            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");

            var user = new User() { Password = "password1234", Username = "" };
            IFluentValidation<User> fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .NotEmpty(u => u.Username)
                .Validate();

            Assert.AreEqual("Username should not be empty", results.First().Condition.Message);
        }

    }
}
