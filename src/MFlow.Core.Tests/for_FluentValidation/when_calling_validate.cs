using Machine.Specifications;
using MEvents.Core;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MFlow.Core;
using MFlow.Core.Events;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_validate : given.a_fluent_user_validator_with_multiple_false_conditions
    {
        static bool raised_validation_failed_event;
        static ICollection<IValidationResult<User>> results;

        Because of = () =>
        {
            event_coordinator.Subscribe<ValidationFailedEvent<User>>(e => { raised_validation_failed_event = true; });
            results = validator.Validate().ToList();
            System.Threading.Thread.Sleep(500);
        };

        It should_contain_the_correct_number_of_validation_results = () => { results.Count.ShouldEqual(3); };
        It should_contain_a_first_result_with_the_correct_key = () => { results.First().Condition.Key.ShouldEqual("Password"); };
        It should_contain_a_second_result_with_the_correct_key = () => { results.Skip(1).First().Condition.Key.ShouldEqual("ConfirmPassword"); };
        It should_contain_a_third_result_with_the_correct_key = () => { results.Skip(2).First().Condition.Key.ShouldEqual("Manager.Username"); };
        It should_contain_a_first_result_with_the_correct_message = () => { results.First().Condition.Message.ShouldEqual("Password should be equal to testingx"); };
        It should_contain_a_second_result_with_the_correct_message = () => { results.Skip(1).First().Condition.Message.ShouldEqual("ConfirmPassword should be equal to testing"); };
        It should_contain_a_third_result_with_the_correct_message = () => { results.Skip(2).First().Condition.Message.ShouldEqual("Manager.Username should not be empty"); };

        It should_raise_a_validation_failed_event = () => { raised_validation_failed_event.ShouldBeTrue(); };

    }
}
