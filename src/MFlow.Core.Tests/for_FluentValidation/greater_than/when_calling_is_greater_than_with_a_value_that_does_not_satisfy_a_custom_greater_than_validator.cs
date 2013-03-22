using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_greater_than_or_equal_with_a_value_that_does_not_satisfy_a_custom_greater_than_or_equal_validator : given.a_fluent_user_validator_with_custom_implementation_set_to_replace
    {

        Because of = () =>
        {
            user.LoginCount = int.MinValue;
            validator.Check(u => u.LoginCount).IsGreaterThanOrEqualTo(100);
        };

        It should_not_be_satisfied = () => { validator.Satisfied().ShouldBeFalse(); };
        It should_return_the_correct_validation_message = () => { validator.Validate().First().Condition.Message.ShouldEqual("LoginCount should be greater than or equal to 100"); };

    }
}
