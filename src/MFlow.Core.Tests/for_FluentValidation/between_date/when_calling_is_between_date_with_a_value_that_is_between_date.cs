using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_between_date_with_a_value_that_is_between_date : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.LastLogin = DateTime.Now;
            validator.Check(u => u.LastLogin).IsBetween(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(2));
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };

    }
}
