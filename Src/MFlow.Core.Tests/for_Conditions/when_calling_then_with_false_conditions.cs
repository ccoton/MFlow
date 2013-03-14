using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_Conditions
{
    public class when_calling_then_true_conditions : given.an_if_or_condition_evaluating_to_false
    {
        static bool executed;

        Because of = () =>
        {
            fluent_conditions.Then(
                    () =>
                    {
                        executed = true;
                    }
                );
        };

        It should_execute_the_then_action = () => { executed.ShouldBeFalse(); };
    }
}
