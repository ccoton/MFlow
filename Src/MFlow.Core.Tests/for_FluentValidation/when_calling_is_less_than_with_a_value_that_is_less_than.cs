using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_less_than_with_a_value_that_is_less_than : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.LoginCount = 1;
            validator.Check(u => u.LoginCount).IsLessThan(10);
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };

    }
}
