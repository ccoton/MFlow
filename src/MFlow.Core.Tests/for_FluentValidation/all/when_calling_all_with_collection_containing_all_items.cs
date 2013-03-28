using Machine.Specifications;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_all_with_collection_containing_all_items : given.a_fluent_user_validator_with_all_users_in_collection
    {
        Because of = () =>
        {
            validator.Check(u => u.Users).All(user1.Users);
        };

        It should_be_satisfied_because_the_collection_contains_all_items = () => { validator.Satisfied().ShouldBeTrue(); };

    }
}
