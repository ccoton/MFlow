﻿using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_post_code_with_a_value_that_is_not_a_post_code : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.Username = "test";
            validator.Check(u => u.Username).IsPostCode();
        };

        It should_not_be_satisfied = () => { validator.Satisfied().ShouldBeFalse(); };
        It should_return_the_correct_validation_message = () => { validator.Validate().First().Condition.Message.ShouldEqual("Username should be a valid post code"); };

    }
}
