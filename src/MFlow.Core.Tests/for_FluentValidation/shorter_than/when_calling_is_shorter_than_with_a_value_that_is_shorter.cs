using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_shorter_than_with_a_value_that_is_shorter : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.Username = "test";
            validator.Check(u => u.Username).IsShorterThan(5);
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };

    }
}
