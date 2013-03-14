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
    }
}
