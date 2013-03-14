using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_greater_than_or_equal_to_with_a_value_that_is_not_greater_than_or_equal : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.LoginCount = 8;
            validator.Check(u => u.LoginCount).IsGreaterThanOrEqualTo(9);
        };

        It should_not_be_satisfied = () => { validator.Satisfied().ShouldBeFalse(); };
        It should_return_the_correct_validation_message = () => { validator.Validate().First().Condition.Message.ShouldEqual("LoginCount should be greater than or equal to 9"); };

    }
}
