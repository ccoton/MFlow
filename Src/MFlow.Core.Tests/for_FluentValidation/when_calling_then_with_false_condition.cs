using Machine.Specifications;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_then_with_false_condition : given.a_fluent_user_validator_with_false_condition
    {
        static bool executed = false;

        Because of = () =>
        {
            ((IFluentValidation<User>)validator).Then(() =>
                {
                    executed = true;
                });
        };

        It should_not_execute_the_then_action = () => { executed.ShouldBeFalse(); };
    }
}
