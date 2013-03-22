using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_date_with_a_value_that_is_not_a_date : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.Username = "123";
            validator.Check(u => u.Username).IsDate();
        };

        It should_not_be_satisfied = () => { validator.Satisfied().ShouldBeFalse(); };
        It should_return_the_correct_validation_message = () => { validator.Validate().First().Condition.Message.ShouldEqual("Username should be a valid date"); };

    }
}
