using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_longer_than_with_a_value_that_is_longer : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.Username = "testing123456";
            validator.Check(u => u.Username).IsLongerThan(10);
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };

    }
}
