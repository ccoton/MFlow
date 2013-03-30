using Machine.Specifications;
using MFlow.Core.Tests.Supporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_none_with_a_value_that_satisfies_a_custom_none_validator : given.a_fluent_user_validator_with_custom_implementation_set_to_replace
    {

        Because of = () =>
        {
            var newUser = new User { Username = "test" };
            user.Username = "test";
            user.Users = new List<User>() { newUser };
            validator.Check(u => u.Users).None(newUser);
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };

    }
}
