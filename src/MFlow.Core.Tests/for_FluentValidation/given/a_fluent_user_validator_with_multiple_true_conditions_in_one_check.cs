using Machine.Specifications;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using MFlow.Core.Validation.Builder;
using MFlow.Core.Validation.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation.given
{
    [Subject("for Fluent Validation")]
    public class a_fluent_user_validator_with_multiple_true_conditions_in_one_check
    {
        protected static IFluentValidationBuilder<User> validator;
        protected static User user; 

        Establish context = () =>
        {
            user = new User();
            user.Manager = new User { Username = "test" };
            validator = new FluentValidationFactory().GetFluentValidation<User>(user);
            user.Password = "test";
            user.ConfirmPassword = "test";
            validator
                .Check(u => u.Password, u => u.ConfirmPassword, u => u.Manager.Username)
                .IsEqualTo("test");
        };
    }
}
