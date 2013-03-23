using Machine.Specifications;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_any_with_collection_containing_item : given.a_fluent_user_validator_with_user_in_collection
    {
        Because of = () =>
        {
            validator.Check(u => u.Users).Any(user);
        };

        It should_be_satisfied_because_the_collection_contains_the_item = () => { validator.Satisfied().ShouldBeTrue(); };

    }
}
