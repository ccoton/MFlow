using Machine.Specifications;
using MFlow.Core.Tests.Supporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_same_with_a_value_that_satisfies_a_custom_is_same_validator : given.a_fluent_user_validator_with_custom_implementation_set_to_replace
    {

        Because of = () =>
        {
            var newUser1 = new User { Username = "test1" };
            var newUser2 = new User { Username = "test2" };
            user.Username = "testUser";
            var newUsers = new List<User>() { newUser1, newUser2 };
            user.Users = new List<User>() { newUser1, newUser2 };
            validator.Check(u => u.Users).IsSame(newUsers);
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };

    }
}
