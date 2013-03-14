using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_Conditions
{
    public class when_calling_else_with_false_conditions : given.an_if_or_condition_evaluating_to_false
    {
        static bool? executed = null;

        Because of = () =>
        {
            fluent_conditions.Then(
                    () =>
                    {
                        executed = true;
                    }
                ).Else(
                    () =>
                    {
                        executed = false;
                    }
                );
        };

        It should_execute_the_else_action = () => { executed.Value.ShouldBeFalse(); };
    }
}
