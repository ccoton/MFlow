using MFlow.Core.Validation.Factories;
using MFlow.Core.Tests.Supporting;
using NUnit.Framework;
using System.Linq;

namespace MFlow.Core.Tests.VmlConfiguration
{
    [TestFixture]
    public partial class VmlConfiguration
    {
        [Test]
        public void Test_Fluent_Validation_NotEmpty_False_Loaded_From_Vml()
        {
            var user = new User();
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "NotEmpty.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_NotEmpty_False_Loaded_From_Vml_Gets_Hint()
        {
            var user = new User();
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "NotEmpty.validation.vml");
            Assert.IsTrue(fluentValidation.Validate().Any(v => v.Condition.Hint == "Enter a username"));
        }

        [Test]
        public void Test_Fluent_Validation_NotEmpty_True_Loaded_From_Vml()
        {
            var user = new User {
                Username = "testing"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "NotEmpty.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsLength_False_Loaded_From_Vml()
        {
            var user = new User();
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsLength.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsLength_True_Loaded_From_Vml()
        {
            var user = new User {
                Username = "12345678901234567890"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsLength.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsNumeric_False_Loaded_From_Vml()
        {
            var user = new User {
                Username = "abc"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsNumeric.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }
        
        [Test]
        public void Test_Fluent_Validation_IsNumeric_True_Loaded_From_Vml()
        {
            var user = new User {
                Username = "123432321"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsNumeric.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsAlpha_False_Loaded_From_Vml()
        {
            var user = new User {
                Username = "123"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsAlpha.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }
        
        [Test]
        public void Test_Fluent_Validation_IsAlpha_True_Loaded_From_Vml()
        {
            var user = new User {
                Username = "abcdefg"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsAlpha.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsLongerThan_False_Loaded_From_Vml()
        {
            var user = new User {
                Username = "12345678901"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsLongerThan.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsLongerThan_True_Loaded_From_Vml()
        {
            var user = new User {
                Username = "123456789012345678901"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsLongerThan.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }
        
        [Test]
        public void Test_Fluent_Validation_IsShorterThan_False_Loaded_From_Vml()
        {
            var user = new User {
                Username = "1234567890123456712345666"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsShorterThan.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsShorterThan_True_Loaded_From_Vml()
        {
            var user = new User {
                Username = "12345678901234567"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsShorterThan.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsCreditCard_False_Loaded_From_Vml()
        {
            var user = new User();
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsCreditCard.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsCreditCard_True_Loaded_From_Vml()
        {
            var user = new User {
                Username = "5105 1051 0510 5100"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsCreditCard.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsPostCode_False_Loaded_From_Vml()
        {
            var user = new User();
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsPostCode.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsPostCode_True_Loaded_From_Vml()
        {
            var user = new User {
                Username = "B69 1TE"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsPostCode.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsZipCode_False_Loaded_From_Vml()
        {
            var user = new User();
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsZipCode.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsZipCode_True_Loaded_From_Vml()
        {
            var user = new User {
                Username = "35801"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsZipCode.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }
        
        [Test]
        public void Test_Fluent_Validation_Contains_False_Loaded_From_Vml()
        {
            var user = new User();
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "Contains.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_Contains_True_Loaded_From_Vml()
        {
            var user = new User {
                Username = "test admin test"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "Contains.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_RegEx_False_Loaded_From_Vml()
        {
            var user = new User {
                Username = "testing"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "RegEx.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_RegEx_True_Loaded_From_Vml()
        {
            var user = new User {
                Username = "user@somedomain.com"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "RegEx.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsEmail_False_Loaded_From_Vml()
        {
            var user = new User {
                Username = "testing"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsEmail.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsEmail_True_Loaded_From_Vml()
        {
            var user = new User {
                Username = "user@somedomain.com"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsEmail.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }
        
        [Test]
        public void Test_Fluent_Validation_IsDate_False_Loaded_From_Vml()
        {
            var user = new User {
                Username = "testing"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsDate.validation.vml");
            Assert.IsFalse(fluentValidation.Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsDate_True_Loaded_From_Vml()
        {
            var user = new User {
                Username = "01/01/2001"
            };
            var fluentValidation = new FluentValidationFactory().GetFluentValidationFromConfig<User>(user, "IsDate.validation.vml");
            Assert.IsTrue(fluentValidation.Satisfied());
        }
    }
}
