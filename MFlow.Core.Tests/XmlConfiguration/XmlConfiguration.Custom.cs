using MFlow.Core.Validation.Factories;
using MFlow.Core.Tests.Supporting;
using NUnit.Framework;

namespace MFlow.Core.Tests.XmlConfiguration
{
    [TestFixture]
    public partial class XmlConfiguration
    {
        [Test]
        public void Test_Fluent_Validation_CustomRule_False_Loaded_From_Xml()
        {
            var user = new User {
                LoginCount = 1
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "CustomRule.validation.xml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }


        [Test]
        public void Test_Fluent_Validation_CustomRule_True_Loaded_From_Xml()
        {
            var user = new User {
                LoginCount = 999
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "CustomRule.validation.xml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }
    }
}
