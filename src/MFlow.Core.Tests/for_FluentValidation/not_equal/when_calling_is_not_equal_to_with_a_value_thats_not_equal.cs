using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_not_equal_to_with_a_value_thats_not_equal : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.Username = "testing";
            validator.Check(u => u.Username).IsNotEqualTo("test");
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };

    }
}
