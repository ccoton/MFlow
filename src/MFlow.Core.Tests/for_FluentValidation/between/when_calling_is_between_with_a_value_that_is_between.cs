using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_between_with_a_value_that_is_between : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.LoginCount = 11;
            validator.Check(u => u.LoginCount).IsBetween(10, 20);
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };

    }
}
