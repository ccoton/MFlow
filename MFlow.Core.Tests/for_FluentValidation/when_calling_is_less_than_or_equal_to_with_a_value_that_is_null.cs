using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_less_than_or_equal_to_with_a_value_that_is_null : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.LockedOutCount = null;
            validator.Check(u => u.LockedOutCount).IsLessThanOrEqualTo(10);
        };

        It should_be_satisfied_as_a_nullable_assumes_default_of_0 = () => { validator.Satisfied().ShouldBeTrue(); };
    }
}
