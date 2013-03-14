using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_before_with_a_date_after_today : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.LastLogin = DateTime.Now.AddDays(10);
            validator.Check(u => u.LastLogin).IsBefore(DateTime.Now);
        };

        It should_not_be_satisfied = () => { validator.Satisfied().ShouldBeFalse(); };
        It should_return_the_correct_validation_message = () => { validator.Validate().First().Condition.Message.ShouldContain("LastLogin should be before"); };

    }
}
