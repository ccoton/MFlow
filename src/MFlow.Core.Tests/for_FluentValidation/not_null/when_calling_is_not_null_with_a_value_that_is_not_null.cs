using Machine.Specifications;
using MFlow.Core.Tests.Supporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_not_null_with_a_value_that_is_not_null : given.a_fluent_user_validator
    {
        Because of = () =>
        {
            user.Manager = new User();
            validator.Check(u => u.Manager).IsNotNull<User>();
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };
    }
}
