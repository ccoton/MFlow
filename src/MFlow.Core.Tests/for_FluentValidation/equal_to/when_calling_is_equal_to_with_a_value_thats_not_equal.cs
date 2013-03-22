using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_equal_to_with_a_value_thats_not_equal : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.Username = "test";
            validator.Check(u => u.Username).IsEqualTo("testing");
        };

        It should_not_be_satisfied = () => { validator.Satisfied().ShouldBeFalse(); };
        It should_return_the_correct_validation_message = () => { validator.Validate().First().Condition.Message.ShouldEqual("Username should be equal to testing"); };

    }
}
