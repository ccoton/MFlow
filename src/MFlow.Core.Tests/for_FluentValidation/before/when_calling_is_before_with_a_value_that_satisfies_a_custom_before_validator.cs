using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_before_with_a_value_that_satisfies_a_custom_before_validator : given.a_fluent_user_validator_with_custom_implementation_set_to_replace
    {

        Because of = () =>
        {
            user.LastLogin = DateTime.Parse("01-01-2000");
            validator.Check(u => u.LastLogin).IsBefore(user.LastLogin.Value);
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };

    }
}
