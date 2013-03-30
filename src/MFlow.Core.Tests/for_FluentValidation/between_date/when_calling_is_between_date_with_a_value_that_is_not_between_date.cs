using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_between_date_with_a_value_that_is_not_between_date : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.LastLogin = DateTime.Parse("01-01-2000");
            validator.Check(u => u.LastLogin).IsBetween(user.LastLogin.Value.AddDays(1), user.LastLogin.Value.AddDays(7));
        };

        It should_not_be_satisfied = () => { validator.Satisfied().ShouldBeFalse(); };
        It should_return_the_correct_first_validation_message = () => { validator.Validate().First().Condition.Message.ShouldEqual("LastLogin should be between 02/01/2000 00:00:00 and 08/01/2000 00:00:00"); };

    }
}
