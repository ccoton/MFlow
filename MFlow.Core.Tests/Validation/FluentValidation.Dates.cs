using System;
using MFlow.Core.Tests.Supporting;
using NUnit.Framework;
using System.Linq;

namespace MFlow.Core.Tests.Validation
{
    public partial class FluentValidation
    {
        [Test]
        public void Test_Fluent_Validation_Before_Valid()
        {
            var user = new User
            {
                LastLogin = DateTime.Now.AddDays(-10)
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                          .Check(u => u.LastLogin).IsBefore(DateTime.Now).Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_Before_InValid()
        {
            var user = new User
            {
                LastLogin = DateTime.Now.AddDays(10)
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                           .Check(u => u.LastLogin).IsBefore(DateTime.Now).Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_Before_InValid_Returns_Message()
        {
            var user = new User
            {
                LastLogin = DateTime.Now.AddDays(10)
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation.Check(u => u.LastLogin).IsBefore(DateTime.Now)
                          .Validate().First().Condition.Message
                          .StartsWith("LastLogin should be before ", StringComparison.Ordinal));
        }

        [Test]
        public void Test_Fluent_Validation_After_Valid()
        {
            var user = new User
            {
                LastLogin = DateTime.Now.AddDays(10)
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                          .Check(u => u.LastLogin).IsAfter(DateTime.Now).Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_After_InValid()
        {
            var user = new User
            {
                LastLogin = DateTime.Now.AddDays(-10)
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                           .Check(u => u.LastLogin).IsAfter(DateTime.Now).Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_After_InValid_Returns_Message()
        {
            var user = new User
            {
                LastLogin = DateTime.Now.AddDays(-10)
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                          .Check(u => u.LastLogin).IsAfter(DateTime.Now)
                          .Validate().First().Condition.Message
                          .StartsWith("LastLogin should be after ", StringComparison.Ordinal));
        }

        [Test]
        public void Test_Fluent_Validation_On_Valid()
        {
            var user = new User
            {
                LastLogin = DateTime.Now
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                          .Check(u => u.LastLogin).IsOn(DateTime.Now).Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_On_InValid()
        {
            var user = new User
            {
                LastLogin = DateTime.Now.AddDays(-10)
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                           .Check(u => u.LastLogin).IsOn(DateTime.Now).Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_On_InValid_Returns_Message()
        {
            var user = new User
            {
                LastLogin = DateTime.Now.AddDays(-10)
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                          .Check(u => u.LastLogin).IsOn(DateTime.Now)
                          .Validate().First().Condition.Message
                          .StartsWith("LastLogin should be on ", StringComparison.Ordinal));
        }

        [Test]
        public void Test_Fluent_Validation_IsThisYear_Valid()
        {
            var user = new User
            {
                LastLogin = DateTime.Now
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                          .Check(u => u.LastLogin).IsThisYear().Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsThisYear_InValid()
        {
            var user = new User
            {
                LastLogin = DateTime.Now.AddYears(1)
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                           .Check(u => u.LastLogin).IsThisYear().Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsThisYear_InValid_Returns_Message()
        {
            var user = new User
            {
                LastLogin = DateTime.Now.AddYears(1)
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                          .Check(u => u.LastLogin).IsThisYear()
                          .Validate().First().Condition.Message
                          .StartsWith("LastLogin should be a date from this year", StringComparison.Ordinal));
        }

        [Test]
        public void Test_Fluent_Validation_IsThisMonth_Valid()
        {
            var user = new User
            {
                LastLogin = DateTime.Now
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                          .Check(u => u.LastLogin).IsThisMonth().Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsThisMonth_InValid()
        {
            var user = new User
            {
                LastLogin = DateTime.Now.AddMonths(1)
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                           .Check(u => u.LastLogin).IsThisMonth().Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsThisMonth_InValid_Returns_Message()
        {
            var user = new User
            {
                LastLogin = DateTime.Now.AddMonths(1)
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                            .Check(u => u.LastLogin).IsThisMonth()
                            .Validate().First().Condition.Message
                            .StartsWith("LastLogin should be a date from this month", StringComparison.Ordinal));
        }

        [Test]
        public void Test_Fluent_Validation_IsThisWeek_Valid()
        {
            var user = new User
            {
                LastLogin = DateTime.Now
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                          .Check(u => u.LastLogin).IsThisWeek().Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsThisWeek_InValid()
        {
            var user = new User
            {
                LastLogin = DateTime.Now.AddDays(7)
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                           .Check(u => u.LastLogin).IsThisWeek().Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsThisWeek_InValid_Returns_Message()
        {
            var user = new User
            {
                LastLogin = DateTime.Now.AddDays(7)
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                            .Check(u => u.LastLogin).IsThisWeek()
                            .Validate().First().Condition.Message
                            .StartsWith("LastLogin should be a date from this week", StringComparison.Ordinal));
        }

        [Test]
        public void Test_Fluent_Validation_IsToday_Valid()
        {
            var user = new User
            {
                LastLogin = DateTime.Now
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                          .Check(u => u.LastLogin).IsToday().Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsToday_InValid()
        {
            var user = new User
            {
                LastLogin = DateTime.Now.AddDays(1)
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                           .Check(u => u.LastLogin).IsToday().Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsToday_InValid_Returns_Message()
        {
            var user = new User
            {
                LastLogin = DateTime.Now.AddDays(1)
            };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                            .Check(u => u.LastLogin).IsToday()
                            .Validate().First().Condition.Message
                            .StartsWith("LastLogin should be todays date", StringComparison.Ordinal));
        }
    }
}
