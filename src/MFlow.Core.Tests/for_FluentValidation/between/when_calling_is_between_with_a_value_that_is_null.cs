using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_between_with_a_value_that_is_null : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.LockedOutCount = null;
            validator.Check(u => u.LockedOutCount).IsBetween(10, 20);
        };

        It should_not_be_satisfied = () => { validator.Satisfied().ShouldBeFalse(); };

        It should_return_the_correct_validation_message = () => { validator.Validate().First().Condition.Message.ShouldEqual("LockedOutCount should be greater than 10"); };
    }
}
