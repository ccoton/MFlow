using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_not_equal_to_with_an_expression_that_is_equal : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.Username = "testing";
            user.Password = "testing";
            validator.Check(u => u.Username).IsNotEqualTo(u=>u.Password);
        };

        It should_not_be_satisfied = () => { validator.Satisfied().ShouldBeFalse(); };
        It should_return_the_correct_validation_message = () => { validator.Validate().First().Condition.Message.ShouldEqual("Username should not be equal to Password"); };

    }
}
