using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_password_with_a_value_that_is_a_valid_password : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.Password = "P8ssw0rd";
            validator.Check(u => u.Password).IsPassword();
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };

    }
}
