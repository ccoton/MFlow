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
        public void Test_Fluent_Validation_Equal()
        {
            var user = new User() { Password = "password123", Username = "testing" };
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Equal(u => u.Username, "testing").Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_Not_Equal()
        {
            var user = new User() { Password = "password123", Username = "testing" };
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .NotEqual(u => u.Username, "testing").Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_LessThan()
        {
            var user = new User() { Password = "password123", Username = "testing", LoginCount = 10 };
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .LessThan(u => u.LoginCount, 11).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_GreaterThan()
        {
            var user = new User() { Password = "password123", Username = "testing", LoginCount = 12 };
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .GreaterThan(u => u.LoginCount, 11).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_DependsOn_Satisfied_Dependency()
        {
            var user = new User() { Password = "password123", Username = "testing", LoginCount = 12 };

            IFluentValidation<User> dependency = new MFlow.Core.Validation.FluentValidation<User>(user);
            dependency.If(true);

            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .GreaterThan(u => u.LoginCount, 11).DependsOn(dependency).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_DependsOn_Unsatisfied_Dependency()
        {
            var user = new User() { Password = "password123", Username = "testing", LoginCount = 12 };

            IFluentValidation<User> dependency = new MFlow.Core.Validation.FluentValidation<User>(user);
            dependency.If(false);

            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .GreaterThan(u => u.LoginCount, 11).DependsOn(dependency).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_RegEx()
        {
            var user = new User() { Password = "password123", Username = "ausername@somedomain.com", LoginCount = 12 };
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .RegEx(u => u.Username, regEx:@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*").Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_IsEmail_With_Valid_Value()
        {
            var user = new User() { Password = "password123", Username = "ausername@somedomain.com", LoginCount = 12 };
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .IsEmail(u => u.Username).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Validation_IsEmail_With_InValid_Value()
        {
            var user = new User() { Password = "password123", Username = "ausername", LoginCount = 12 };
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .IsEmail(u => u.Username).Satisfied());
        }
    }
}
