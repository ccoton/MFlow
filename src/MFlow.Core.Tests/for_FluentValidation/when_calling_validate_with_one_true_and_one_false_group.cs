using Machine.Specifications;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_validate_with_one_true_and_one_false_group : given.a_fluent_user_validator_with_one_true_and_one_false_group
    {

        static ICollection<IValidationResult<User>> group_one_results;
        static ICollection<IValidationResult<User>> group_two_results;

        Because of = () => { 
            group_one_results = validator.Validate("Credentials").ToList();
            group_two_results = validator.Validate("Username").ToList(); 
        };

        It should_contain_no_validation_results_for_true_group = () => { group_one_results.Any().ShouldBeFalse(); };

        It should_contain_the_correct_number_of_validation_results_for_false_group = () => { group_two_results.Count.ShouldEqual(2); };
        It should_contain_a_first_result_with_the_correct_key_for_false_group = () => { group_two_results.First().Condition.Key.ShouldEqual("Username"); };
        It should_contain_a_second_result_with_the_correct_key_for_false_group = () => { group_two_results.Skip(1).First().Condition.Key.ShouldEqual("Username"); };
        It should_contain_a_first_result_with_the_correct_message_for_false_group = () => { group_two_results.First().Condition.Message.ShouldEqual("Username should be equal to testingx"); };
        It should_contain_a_second_result_with_the_correct_message_for_false_group = () => { group_two_results.Skip(1).First().Condition.Message.ShouldEqual("Username should be equal to testing"); };
 
    }
}
