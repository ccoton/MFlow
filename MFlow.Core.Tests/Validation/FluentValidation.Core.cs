using System;
using MFlow.Core.Conditions;
using MFlow.Core.Events;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;
using System.Globalization;
using MFlow.Core.Conditions.Enums;

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
            var fluentValidation = _factory.GetFluentValidation<User>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Chained_Fluent_Validation_ValidateAndThrow_InValid()
        {
            var user = new User() { Password = "password123", Username = "testing" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            fluentValidation
                .Check(u => u.Username).IsEqualTo("xxx").ValidateAndThrow<ArgumentException>();
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_ValidateAndThrow_Valid()
        {
            var user = new User() { Password = "password123", Username = "xxx" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Username).IsEqualTo("xxx").ValidateAndThrow<ArgumentException>().Any());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_InValid_Expression()
        {
            var user = new User() { Password = "password123", Username = "testing" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Username).IsEqualTo("xxx").Satisfied());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_Valid_Expression_Chain()
        {
            var user = new User() { Password = "password123", Username = "testing" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.Username).IsEqualTo("testing")
                .Check(u => u.Password).IsEqualTo("password123").Satisfied());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_InValid_Expression_Chain()
        {
            var user = new User() { Password = "password123", Username = "testingx" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Username).IsEqualTo("testing")
                .Check(u => u.Password).IsEqualTo("password123").Satisfied());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_Valid_Or_Expression_Chain()
        {
            var user = new User() { Password = "password1213", Username = "testingx" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.Username).IsEqualTo("testing")
                .Check(u => u.Password).IsEqualTo("password123")
                .Check(u => u.Username, conditionType: ConditionType.Or).IsEqualTo("testingx").Satisfied());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_InValid_Or_Expression_Chain()
        {
            var user = new User() { Password = "password1234", Username = "testingx" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Username).IsEqualTo("testing")
                .Check(u => u.Password).IsEqualTo("password123")
                .Check(u => u.Username, conditionType: ConditionType.Or).IsEqualTo("test").Satisfied());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_With_Valid_Expression_Executes()
        {
            var user = new User() { Password = "password123", Username = "testing" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            fluentValidation
                .Check(u => u.Username).IsEqualTo("testing")
                .Check(u => u.Password).IsEqualTo("password123")
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
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            fluentValidation
                .Check(u => u.Username).IsEqualTo("testing")
                .Check(u => u.Password).IsEqualTo("password123")
                .Then(() => { user.Username = "valid"; })
                .Else(() => { user.Username = "invalid"; });

            Assert.IsTrue(user.Username == "invalid");
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Raises_Event()
        {
            var user = new User() { Password = "password123", Username = "testing" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);

            var events = new EventsFactory().GetEventStore();
            events.Register<UserCreatedEvent>(s =>
                {
                    s.Source.Username = "caught event";
                    user = s.Source;
                });

            fluentValidation
                 .Check(u => u.Username).IsEqualTo("testing")
                .Check(u => u.Password).IsEqualTo("password123")
                .Raise(new UserCreatedEvent(user));

            Assert.AreEqual(user.Username, "caught event");
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Returns_Correct_Number_Of_Results()
        {
            var user = new User() { Password = "password123", Username = "testingx" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).IsEqualTo("testing")
                .Check(u => u.Password).IsEqualTo("password123").Validate();

            Assert.AreEqual(1, results.ToList().Count());

        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Returns_Correct_Key()
        {
            var user = new User() { Password = "password123", Username = "testingx" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).IsEqualTo("testing")
                .Check(u => u.Password).IsEqualTo("password123").Validate();

            Assert.AreEqual("Username", results.First().Condition.Key);

        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Returns_Correct_Message()
        {
            var user = new User() { Password = "password123", Username = "testingx" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).IsEqualTo("testing").Message("Username is not valid")
                .Check(u => u.Password).IsEqualTo("password123").Validate();

            Assert.AreEqual("Username is not valid", results.First().Condition.Message);

        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Returns_Correct_Number_Of_Multipe_Results()
        {
            var user = new User() { Password = "password1234", Username = "testingx" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).IsEqualTo("testing")
                .Check(u => u.Password).IsEqualTo("password123").Validate();

            Assert.AreEqual(2, results.ToList().Count());

        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Returns_Correct_Number_Of_Multipe_Results_With_Warnings_Supressed()
        {
            var user = new User() { Password = "password1234", Username = "testingx" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).IsEqualTo("testing")
                .Check(u => u.Username, output: ConditionOutput.Warning).IsEqualTo("abc")
                .Check(u => u.Password).IsEqualTo("password123").Validate();

            Assert.AreEqual(2, results.ToList().Count());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Returns_Correct_Number_Of_Multipe_Results_With_Warnings()
        {
            var user = new User() { Password = "password1234", Username = "testingx" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).IsEqualTo("testing")
                .Check(u => u.Username, output: ConditionOutput.Warning).IsEqualTo("abc")
                .Check(u => u.Password).IsEqualTo("password123").Validate(supressWarnings:false);

            Assert.AreEqual(3, results.ToList().Count());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Returns_Correct_Keys()
        {
            var user = new User() { Password = "password123", Username = "testingx" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).IsEqualTo("testing")
                .Check(u => u.Password).IsEqualTo("password1234").Validate();

            Assert.AreEqual("Username", results.First().Condition.Key);
            Assert.AreEqual("Password", results.Skip(1).Take(1).First().Condition.Key);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Returns_Correct_Messages()
        {
            var user = new User() { Password = "password1234", Username = "testingx" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).IsEqualTo("testing").Message("Username is not valid")
                .Check(u => u.Password).IsEqualTo("password123").Message("Password is now valid").Validate();

            Assert.AreEqual("Username is not valid", results.First().Condition.Message);
            Assert.AreEqual("Password is now valid", results.Skip(1).Take(1).First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Returns_Correct_Complex_Keys()
        {
            var fluentValidation = _factory.GetFluentValidation<Thread>(Thread.CurrentThread);
            var results = fluentValidation
                .Check(u => u.CurrentCulture.DisplayName).IsEqualTo("")
                .Check(u => u.CurrentCulture.EnglishName).IsEqualTo("").Validate();

            Assert.AreEqual("CurrentCulture.DisplayName", results.First().Condition.Key);
            Assert.AreEqual("CurrentCulture.EnglishName", results.Skip(1).Take(1).First().Condition.Key);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_NotNullOrEmpty_With_Valid_Value()
        {
            var user = new User() { Password = "password1234", Username = "testingx" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).IsNotEmpty().Message("Username is not valid")
                .Validate();

            Assert.AreEqual(0, results.Count());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_NotNullOrEmpty_Message_Lookup()
        {
            var user = new User() { Password = "password1234", Username = "" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).IsNotEmpty()
                .Validate();

            Assert.AreEqual("Username should not be empty", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Equal_Message_Lookup()
        {
            var user = new User() { Password = "password1234", Username = "" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).IsEqualTo("test")
                .Validate();

            Assert.AreEqual("Username should be equal to test", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_NotEqual_Message_Lookup()
        {
            var user = new User() { Password = "password1234", Username = "test" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).IsNotEqualTo("test")
                .Validate();

            Assert.AreEqual("Username should not be equal to test", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_LessThan_Message_Lookup()
        {
            var user = new User() { LoginCount = 11 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.LoginCount).IsLessThan(10)
                .Validate();

            Assert.AreEqual("LoginCount should be less than 10", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_LessThanOrEqualTo_Message_Lookup()
        {
            var user = new User() { LoginCount = 11 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.LoginCount).IsLessThanOrEqualTo(10)
                .Validate();

            Assert.AreEqual("LoginCount should be less than or equal to 10", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_GreaterThan_Message_Lookup()
        {
            var user = new User() { LoginCount = 9 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.LoginCount).IsGreaterThan(10)
                .Validate();

            Assert.AreEqual("LoginCount should be greater than 10", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_GreaterThanOrEqualTo_Message_Lookup()
        {
            var user = new User() { LoginCount = 9 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.LoginCount).IsGreaterThanOrEqualTo(10)
                .Validate();

            Assert.AreEqual("LoginCount should be greater than or equal to 10", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Email_Message_Lookup()
        {
            var user = new User() { Username = "fred" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).IsEmail()
                .Validate();

            Assert.AreEqual("Username should be an email", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Contains_Message_Lookup()
        {
            var user = new User() { Username = "fred" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).Contains("dave")
                .Validate();

            Assert.AreEqual("Username should contain dave", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_IsLength_Message_Lookup()
        {
            var user = new User() { Username = "fred" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).IsLength(5)
                .Validate();

            Assert.AreEqual("Username should be 5 characters long", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_IsRequired_Message_Lookup()
        {
            var user = new User() { Username = "" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).IsRequired<string>()
                .Validate();

            Assert.AreEqual("Username is a required field", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Before_Message_Lookup()
        {
            var user = new User() { LastLogin = DateTime.Now };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.LastLogin).IsBefore(DateTime.Parse("01/01/2001 00:00:00"))
                .Validate();

            Assert.AreEqual("LastLogin should be before 01/01/2001 00:00:00", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_After_Message_Lookup()
        {
            var user = new User() { LastLogin = DateTime.Parse("01/01/2001 00:00:00") };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.LastLogin).IsAfter(DateTime.Parse("01/01/2012 00:00:00"))
                .Validate();

            Assert.AreEqual("LastLogin should be after 01/01/2012 00:00:00", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_On_Message_Lookup()
        {
            var user = new User() { LastLogin = DateTime.Parse("01/01/2001 00:00:00") };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.LastLogin).IsOn(DateTime.Parse("01/01/2012 00:00:00"))
                .Validate();

            Assert.AreEqual("LastLogin should be on 01/01/2012 00:00:00", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_On_Message_Lookup_Different_Culture()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");

            var user = new User() { LastLogin = DateTime.Parse("01/01/2001 00:00:00") };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.LastLogin).IsOn(DateTime.Parse("01/01/2012 00:00:00"))
                .Validate();

            Assert.AreEqual("LastLogin doit être mis sur 01/01/2012 00:00:00", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Message_Lookup_Missing_Culture()
        {

            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");

            var user = new User() { Password = "password1234", Username = "" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).IsNotEmpty()
                .Validate();

            Assert.AreEqual("Username should not be empty", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Fluent_Validation_IsEmail_False_Loaded_From_Xml()
        {
            var user = new User() { Username = "testing" };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "IsEmail.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Email_Fluent_Message()
        {
            var user = new User() { Username = "fred" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).IsEmail()
                .Message("The username should be some kind of valid email address")
                .Validate();

            Assert.AreEqual("The username should be some kind of valid email address", results.First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Email_Chained_Fluent_Message()
        {
            var user = new User() { Username = "fred" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).IsEmail()
                .Message("The username should be some kind of valid email address")
                .Check(u => u.Password).IsNotEmpty()
                .Message("The password should not be empty or null")
                .Validate();

            Assert.AreEqual("The password should not be empty or null", results.Skip(1).Take(1).First().Condition.Message);
        }

        [TestMethod]
        public void Test_Chained_Fluent_Validation_Email_Chained_Fluent_Custom_Message()
        {
            var user = new User() { Username = "fred" };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).IsEmail()
                .Message("$ACustomMessage$")
                .Validate();

            Assert.AreEqual("Something different here", results.First().Condition.Message);
        }
    }
}
