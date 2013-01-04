﻿using System;
using MFlow.Core.Conditions;
using MFlow.Core.Events;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;

namespace MFlow.Core.Tests.Validation
{
    public partial class FluentValidation
    {
        [TestMethod]
        public void Test_Fluent_Validation_Before_Valid()
        {
            var user = new User() { LastLogin = DateTime.Now.AddDays(-10),  Password = "password123", Username = "testing", LoginCount = 10 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.LastLogin).IsBefore(DateTime.Now).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_Before_InValid()
        {
            var user = new User() { LastLogin = DateTime.Now.AddDays(10), Password = "password123", Username = "testing", LoginCount = 10 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.LastLogin).IsBefore(DateTime.Now).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_After_Valid()
        {
            var user = new User() { LastLogin = DateTime.Now.AddDays(10), Password = "password123", Username = "testing", LoginCount = 10 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.LastLogin).IsAfter(DateTime.Now).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_After_InValid()
        {
            var user = new User() { LastLogin = DateTime.Now.AddDays(-10), Password = "password123", Username = "testing", LoginCount = 10 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.LastLogin).IsAfter(DateTime.Now).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_On_Valid()
        {
            var user = new User() { LastLogin = DateTime.Now, Password = "password123", Username = "testing", LoginCount = 10 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.LastLogin).IsOn(DateTime.Now).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_On_InValid()
        {
            var user = new User() { LastLogin = DateTime.Now.AddDays(-10), Password = "password123", Username = "testing", LoginCount = 10 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.LastLogin).IsOn(DateTime.Now).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_IsThisYear_Valid()
        {
            var user = new User() { LastLogin = DateTime.Now, Password = "password123", Username = "testing", LoginCount = 10 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.LastLogin).IsThisYear().Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_IsThisYear_InValid()
        {
            var user = new User() { LastLogin = DateTime.Now.AddYears(1), Password = "password123", Username = "testing", LoginCount = 10 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.LastLogin).IsThisYear().Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_IsThisMonth_Valid()
        {
            var user = new User() { LastLogin = DateTime.Now, Password = "password123", Username = "testing", LoginCount = 10 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.LastLogin).IsThisMonth().Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_IsThisMonth_InValid()
        {
            var user = new User() { LastLogin = DateTime.Now.AddMonths(1), Password = "password123", Username = "testing", LoginCount = 10 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.LastLogin).IsThisMonth().Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_IsThisWeek_Valid()
        {
            var user = new User() { LastLogin = DateTime.Now, Password = "password123", Username = "testing", LoginCount = 10 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.LastLogin).IsThisWeek().Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_IsThisWeek_InValid()
        {
            var user = new User() { LastLogin = DateTime.Now.AddDays(7), Password = "password123", Username = "testing", LoginCount = 10 };
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.LastLogin).IsThisWeek().Satisfied());
        }
    }
}
