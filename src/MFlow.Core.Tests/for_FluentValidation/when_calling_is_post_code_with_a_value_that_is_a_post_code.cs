using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_post_code_with_a_value_that_is_a_post_code : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.Username = "B69 1TE";
            validator.Check(u => u.Username).IsPostCode();
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };

    }
}
