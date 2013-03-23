using Machine.Specifications;
using MFlow.Core.Tests.Supporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_not_null_with_a_value_that_is_null : given.a_fluent_user_validator
    {
        Because of = () =>
        {
            validator.Check(u => u.Manager).IsNotNull<User>();
        };

        It should_not_be_satisfied = () => { validator.Satisfied().ShouldBeFalse(); };
        It should_return_the_correct_validation_message = () => { validator.Validate().First().Condition.Message.ShouldEqual("Manager should not be null"); };

    }
}
