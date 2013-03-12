using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_matches_with_a_value_that_is_null : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.Username = null;
            validator.Check(u => u.Username).Matches(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        };

        It should_not_be_satisfied = () => { validator.Satisfied().ShouldBeFalse(); };
        It should_return_the_correct_validation_message = () => { validator.Validate().First().Condition.Message.ShouldEqual(@"Username should validate expression \w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"); };

    }
}
