using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_contains_with_a_value_that_contains : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.Username = "someemail";
            validator.Check(u => u.Username).Contains("email");
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };

    }
}
