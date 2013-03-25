using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_between_date_with_a_value_that_is_null : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            var date = DateTime.Parse("01-01-2000");
            user.LastLogin = null;
            validator.Check(u => u.LastLogin).IsBetween(date, date.AddDays(5));
        };

        It should_not_be_satisfied = () => { validator.Satisfied().ShouldBeFalse(); };

        It should_return_the_correct_validation_message = () => { validator.Validate().First().Condition.Message.ShouldEqual("LastLogin should be between 01/01/2000 00:00:00 and 06/01/2000 00:00:00"); };
    }
}
