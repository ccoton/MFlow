using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_alpha_with_a_value_that_is_alpha : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.Username = "ABC";
            validator.Check(u => u.Username).IsAlpha();
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };

    }
}
