using Machine.Specifications;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_raising_an_event : given.a_fluent_user_validator_with_true_condition
    {

        Because of = () =>
        {
            ((IFluentValidation<User>)validator).Raise(new UserCreatedEvent(user));

            // Hate to do this by in order to check a validated event is published, 
            // since its published on another thread we just need to give it a moment.
            Thread.Sleep(300);
        };

        It should_raise_the_event = () => { user.Username.ShouldEqual("raised event"); };

    }
}
