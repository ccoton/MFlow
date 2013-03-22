using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_post_code_with_a_value_that_satisfies_a_custom_post_code_validator : given.a_fluent_user_validator_with_custom_implementation_set_to_replace
    {

        Because of = () =>
        {
            user.Password = "custompostcodevalidator";
            validator.Check(u => u.Password).IsPostCode();
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };

    }
}
