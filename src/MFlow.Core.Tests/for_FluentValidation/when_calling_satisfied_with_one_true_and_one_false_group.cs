using Machine.Specifications;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_satisfied_with_one_true_and_one_false_group : given.a_fluent_user_validator_with_one_true_and_one_false_group
    {

        static bool group_one_results;
        static bool group_two_results;

        Because of = () => { 
            group_one_results = validator.Satisfied("Credentials");
            group_two_results = validator.Satisfied("Username"); 
        };

        It should_return_true_for_the_true_group = () => { group_one_results.ShouldBeTrue(); };
        It should_return_false_for_the_false_group = () => { group_two_results.ShouldBeFalse(); };
    }
}
