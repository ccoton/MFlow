using Machine.Specifications;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using System.Collections.Generic;
using System.Linq;
using MFlow.Core.Events;
using System.Threading;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_validate_for_fr_FR_culture : given.a_fluent_user_validator_with_multiple_false_conditions_for_fr_FR_culture
    {
        static bool raised_validation_failed_event;
        static ICollection<IValidationResult<User>> results;

        Because of = () =>
        {
            event_coordinator.Subscribe<ValidationFailedEvent<User>>(e => { raised_validation_failed_event = true; });
            results = validator.Validate().ToList();

            // reset culture back to default so other test run as expected
            Thread.CurrentThread.CurrentUICulture = default_culture;
        };

        It should_contain_the_correct_number_of_validation_results = () => { results.Count.ShouldEqual(3); };
        It should_contain_a_first_result_with_the_correct_key = () => { results.First().Condition.Key.ShouldEqual("Password"); };
        It should_contain_a_second_result_with_the_correct_key = () => { results.Skip(1).First().Condition.Key.ShouldEqual("ConfirmPassword"); };
        It should_contain_a_third_result_with_the_correct_key = () => { results.Skip(2).First().Condition.Key.ShouldEqual("Manager.Username"); };
        It should_contain_a_first_result_with_the_correct_message = () => { results.First().Condition.Message.ShouldEqual("Password doit être égale à testingx"); };
        It should_contain_a_second_result_with_the_correct_message = () => { results.Skip(1).First().Condition.Message.ShouldEqual("ConfirmPassword doit être égale à testing"); };
        It should_contain_a_third_result_with_the_correct_message = () => { results.Skip(2).First().Condition.Message.ShouldEqual("Manager.Username ne doit pas être vide"); };

        It should_raise_a_validation_failed_event = () => { raised_validation_failed_event.ShouldBeTrue(); };

    }
}
