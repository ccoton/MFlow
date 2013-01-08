using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using NUnit.Framework;

namespace MFlow.Core.Tests.VmlConfiguration
{
    [TestFixture]
    public partial class VmlConfiguration
    {
        [Test]
        public void Test_Fluent_Validation_CustomRule_False_Loaded_From_Vml()
        {
            var user = new User() { LoginCount = 1 };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "CustomRule.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_CustomRule_True_Loaded_From_Vml()
        {
            var user = new User() { LoginCount = 999 };
            var fluentValidation = new FluentValidationFactory().GetFluentValidation<User>(user, true, "CustomRule.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }
    }
}
