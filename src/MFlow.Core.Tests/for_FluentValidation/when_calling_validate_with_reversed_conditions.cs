using Machine.Specifications;
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
    public class when_calling_validate_with_reversed_conditions : given.a_fluent_user_validator_with_multiple_false_reversed_conditions
    {
        static bool raised_valided_event;
        static ICollection<IValidationResult<User>> results;

        Because of = () => {
            event_coordinator.Subscribe<ValidatedEvent<User>>(e => { raised_valided_event = true; });
            results = validator.Validate().ToList(); 
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };
        It should_raise_a_validated_event = () => { raised_valided_event.ShouldBeTrue(); };
 
    }
}
