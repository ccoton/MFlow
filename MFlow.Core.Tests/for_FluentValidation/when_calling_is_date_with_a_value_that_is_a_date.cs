using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_date_with_a_value_that_is_a_date : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.Username = "01-01-2001";
            validator.Check(u => u.Username).IsDate();
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };

    }
}
