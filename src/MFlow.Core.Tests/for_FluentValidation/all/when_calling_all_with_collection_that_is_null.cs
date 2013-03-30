using Machine.Specifications;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_all_with_collection_that_is_null : given.a_fluent_user_validator_with_user_not_in_collection
    {
        Because of = () =>
        {
            user.Users = null;
            var newUsers = new List<User>() { new User() { Username = "test" } };
            validator.Check(u => u.Users).All(newUsers);
        };

        It should_be_not_be_satisfied_because_the_collection_does_not_contain_the_item = () => { validator.Satisfied().ShouldBeFalse(); };
        It should_return_the_correct_validation_message = () => { validator.Validate().First().Condition.Message.ShouldEqual("Users should contain all the items specified"); };

    }
}
