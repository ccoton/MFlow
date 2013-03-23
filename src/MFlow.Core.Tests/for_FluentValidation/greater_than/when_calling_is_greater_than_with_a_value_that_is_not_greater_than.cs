using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_greater_than_with_a_value_that_is_not_greater_than : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.LoginCount = 7;
            validator.Check(u => u.LoginCount).IsGreaterThan(9);
        };

        It should_not_be_satisfied = () => { validator.Satisfied().ShouldBeFalse(); };
        It should_return_the_correct_validation_message = () => { validator.Validate().First().Condition.Message.ShouldEqual("LoginCount should be greater than 9"); };

    }
}
