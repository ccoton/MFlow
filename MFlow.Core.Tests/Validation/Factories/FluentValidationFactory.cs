using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using MFlow.Core.Validation.Builder;
using MFlow.Core.Validation.Factories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.Validation.Factories
{
    [TestFixture]
    public class FluentValidationFactory
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Fluent_Validation_Factory_With_Null_Target_Throws_Exception()
        {
            IFluentValidationFactory factory = new MFlow.Core.Validation.Factories.FluentValidationFactory();
            factory.GetFluentValidation<User>(null);
        }

        [Test]
        public void Test_Fluent_Validation_Factory_With_Target_Returns_Correct_Type()
        {
            var type = new MFlow.Core.Validation.Factories.FluentValidationFactory().GetFluentValidation(new User());
            Assert.IsInstanceOf<IFluentValidationBuilder<User>>(type);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Fluent_Validation_Factory_With_Null_Target_Throws_Exception_With_Config()
        {
            var type = new MFlow.Core.Validation.Factories.FluentValidationFactory().GetFluentValidationFromConfig<User>(null, "IsDate.validation.xml");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Fluent_Validation_Factory_With_Null_Target_Throws_Exception_With_Empty_Config()
        {
            var type = new MFlow.Core.Validation.Factories.FluentValidationFactory().GetFluentValidationFromConfig(new User(), string.Empty);
        }

        [Test]
        public void Test_Fluent_Validation_Factory_With_Target_Returns_Correct_Type_With_Config()
        {
            var type = new MFlow.Core.Validation.Factories.FluentValidationFactory().GetFluentValidationFromConfig(new User(), "IsDate.validation.xml");
            Assert.IsInstanceOf<IFluentValidationBuilder<User>>(type);
        }
    }
}
