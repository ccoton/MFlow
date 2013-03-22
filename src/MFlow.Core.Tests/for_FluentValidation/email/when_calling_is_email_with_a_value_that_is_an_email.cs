using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_email_with_a_value_that_is_an_email : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.Username = "someemail@somedomain.com";
            validator.Check(u => u.Username).IsEmail();
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };

    }
}
