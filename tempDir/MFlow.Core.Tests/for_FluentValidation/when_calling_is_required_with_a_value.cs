using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_required_with_a_value : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.Password = "password";
            validator.Check(u => u.Password).IsRequired<string>();
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };
    }
}
