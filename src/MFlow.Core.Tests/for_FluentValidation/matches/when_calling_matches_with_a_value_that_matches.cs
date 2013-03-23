using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_matches_with_a_value_that_matches : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.Username = "someemail@somedomain.com";
            validator.Check(u => u.Username).Matches(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };

    }
}
