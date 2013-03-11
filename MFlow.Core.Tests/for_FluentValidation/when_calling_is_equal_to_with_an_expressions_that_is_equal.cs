using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_equal_to_with_an_expression_that_is_equal : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.Username = "test";
            user.Password = "test";
            validator.Check(u => u.Username).IsEqualTo(u=>u.Password);
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };
    }
}
