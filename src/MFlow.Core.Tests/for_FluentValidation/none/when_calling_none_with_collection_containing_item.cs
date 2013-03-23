using Machine.Specifications;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_none_with_collection_containing_item : given.a_fluent_user_validator_with_user_in_collection
    {
        Because of = () =>
        {
            validator.Check(u => u.Users).None(user);
        };

        It should_not_be_satisfied_because_the_collection_contains_the_item = () => { validator.Satisfied().ShouldBeFalse(); };
        It should_return_the_correct_validation_message = () => { validator.Validate().First().Condition.Message.ShouldEqual("Users should not contain the item specified"); };
    }
}
