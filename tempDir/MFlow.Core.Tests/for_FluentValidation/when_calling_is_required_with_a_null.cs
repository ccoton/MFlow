using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_required_with_a_null : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.Password = null;
            validator.Check(u => u.Password).IsRequired<string>();
        };

        It should_not_be_satisfied = () => { validator.Satisfied().ShouldBeFalse(); };
        It should_return_the_correct_validation_message = () => { validator.Validate().First().Condition.Message.ShouldContain("Password is a required field"); };

    }
}
