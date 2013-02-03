using System;
using MFlow.Core.Validation.Factories;
using MFlow.Core.Tests.Supporting;
using NUnit.Framework;

namespace MFlow.Core.Tests.VmlConfiguration
{
    [TestFixture]
    public partial class VmlConfiguration
    {
        [Test]
        public void Test_Fluent_Validation_After_False_Loaded_From_Vml()
        {
            var user = new User {
                LastLogin = DateTime.Parse("01/02/2010")
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "After.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_After_True_Loaded_From_Vml()
        {
            var user = new User {
                LastLogin = DateTime.Parse("01/02/2020")
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "After.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_Before_False_Loaded_From_Vml()
        {
            var user = new User {
                LastLogin = DateTime.Parse("01/02/2020")
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "Before.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_Before_True_Loaded_From_Vml()
        {
            var user = new User {
                LastLogin = DateTime.Parse("01/02/2010")
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "Before.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_On_False_Loaded_From_Vml()
        {
            var user = new User {
                LastLogin = DateTime.Parse("01/02/2015")
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "On.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_On_True_Loaded_From_Vml()
        {
            var user = new User {
                LastLogin = DateTime.Parse("01/01/2015")
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "On.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsThisYear_False_Loaded_From_Vml()
        {
            var user = new User {
                LastLogin = DateTime.Now.AddYears(1)
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsThisYear.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsThisYear_True_Loaded_From_Vml()
        {
            var user = new User {
                LastLogin = DateTime.Now
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsThisYear.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsThisMonth_False_Loaded_From_Vml()
        {
            var user = new User {
                LastLogin = DateTime.Now.AddMonths(1)
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsThisMonth.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsThisMonth_True_Loaded_From_Vml()
        {
            var user = new User {
                LastLogin = DateTime.Now
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsThisMonth.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsThisWeek_False_Loaded_From_Vml()
        {
            var user = new User {
                LastLogin = DateTime.Now.AddDays(7)
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsThisWeek.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsThisWeek_True_Loaded_From_Vml()
        {
            var user = new User {
                LastLogin = DateTime.Now
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsThisWeek.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsToday_False_Loaded_From_Vml()
        {
            var user = new User {
                LastLogin = DateTime.Now.AddDays(1)
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsToday.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsToday_True_Loaded_From_Vml()
        {
            var user = new User {
                LastLogin = DateTime.Now
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsToday.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }
    }
}
