using Machine.Specifications;
using MEvents.Core;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using MFlow.Core.Validation.Builder;
using MFlow.Core.Validation.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation.given
{
    [Subject("for Fluent Validation")]
    public class a_fluent_user_validator_with_true_condition
    {
        protected static IFluentValidationBuilder<User> validator;
        protected static User user; 

        Establish context = () =>
        {
            var events = new EventsFactory().GetEventCoordinator();
            events.Subscribe<UserCreatedEvent>(s =>
            {
                s.Source.Username = "raised event";
                user = s.Source;
            });

            user = new User();
            validator = new FluentValidationFactory().GetFluentValidation<User>(user);
            user.Password = "testing";
            validator.Check(u => u.Password).IsEqualTo("testing");
        };
    }
}
