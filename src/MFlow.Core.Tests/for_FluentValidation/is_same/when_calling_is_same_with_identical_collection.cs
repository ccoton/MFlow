using Machine.Specifications;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_same_with_identical_collection : given.a_fluent_user_validator_with_all_users_in_collection
    {
        Because of = () =>
        {
            validator.Check(u => u.Users).IsSame(user1.Users);
        };

        It should_be_satisfied_because_the_collection_contains_all_items = () => { validator.Satisfied().ShouldBeTrue(); };

    }
}
