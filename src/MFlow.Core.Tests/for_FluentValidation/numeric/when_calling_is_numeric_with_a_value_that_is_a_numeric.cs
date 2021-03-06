﻿using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_numeric_with_a_value_that_is_numeric : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.Username = "35801";
            validator.Check(u => u.Username).IsNumeric();
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };

    }
}
