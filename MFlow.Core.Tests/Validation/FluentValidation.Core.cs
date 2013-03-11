using System;
using MFlow.Core.Events;
using MFlow.Core.Validation.Factories;
using MFlow.Core.Tests.Supporting;
using System.Linq;
using System.Threading;
using System.Globalization;
using MFlow.Core.Conditions.Enums;
using NUnit.Framework;
using MFlow.Core.Validation.Builder;

namespace MFlow.Core.Tests.Validation
{
    [TestFixture]
    public partial class FluentValidation
    {

        readonly IFluentValidationFactory _factory = new FluentValidationFactory();


        [Test]
        public void Test_Chained_Fluent_Validation_Returns_Correct_Complex_Keys()
        {
            var fluentValidation = _factory.GetFluentValidation<Thread>(Thread.CurrentThread);
            var results = fluentValidation
                .Check(u => u.CurrentCulture.DisplayName).IsEqualTo("")
                .Check(u => u.CurrentCulture.EnglishName).IsEqualTo("").Validate();

            Assert.AreEqual("CurrentCulture.DisplayName", results.First().Condition.Key);
            Assert.AreEqual("CurrentCulture.EnglishName", results.Skip(1).Take(1).First().Condition.Key);
        }

        [Test]
        public void Test_Chained_Fluent_Validation_NotNullOrEmpty_With_Valid_Value()
        {
            var user = new User {
                Password = "password1234",
                Username = "testingx"
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).IsNotEmpty().Message("Username is not valid")
                .Validate();

            Assert.AreEqual(0, results.Count());
        }

        [Test]
        public void Test_Chained_Fluent_Validation_NotNullOrEmpty_Message_Lookup()
        {
            var user = new User {
                Password = "password1234",
                Username = ""
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).IsNotEmpty()
                .Validate();

            Assert.AreEqual("Username should not be empty", results.First().Condition.Message);
        }

        [Test]
        public void Test_Chained_Fluent_Validation_Email_Chained_Fluent_Custom_Message()
        {
            var user = new User {
                Username = "fred"
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).IsEmail()
                .Message("$ACustomMessage$")
                .Validate();

            Assert.AreEqual("Something different here", results.First().Condition.Message);
        }
        
        [Test]
        public void Test_Chained_Fluent_Validation_Multiple_Validation_Instances()
        {
            var user = new User {
                Username = "fred"
            };
            var emptyUser = new User {
                Username = ""
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var secondFluentValidation = _factory.GetFluentValidation<User>(emptyUser);
            fluentValidation.Check(u => u.Username).IsNotEmpty();
            secondFluentValidation.Check(u => u.Username).IsNotEmpty();
            
            var first = fluentValidation.Satisfied();
            var second = secondFluentValidation.Satisfied();
            
            Assert.IsTrue(first && !second);
            
        }

        [Test]
        public void Test_Chained_Fluent_Validation_Hint()
        {
            var user = new User {
                Username = "fred"
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).IsEmail()
                .Message("Some kind of message")
                .Hint("A different hint message")
                .Validate();
            
            Assert.AreEqual("A different hint message", results.First().Condition.Hint);
        }

        [Test]
        public void Test_Chained_Fluent_Validation_Custom_Hint()
        {
            var user = new User {
                Username = "fred"
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            var results = fluentValidation
                .Check(u => u.Username).IsEmail()
                .Message("Some kind of message")
                .Hint("$ACustomMessage$")
                .Validate();
            
            Assert.AreEqual("Something different here", results.First().Condition.Hint);
        }
    }
}
