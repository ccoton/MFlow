using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_Conditions
{
    public class when_calling_satisfied_with_multiple_false_conditions : given.multiple_if_conditions_evaluating_to_false
    {
        static bool result;

        Because of = () =>
        {
            result = fluent_conditions.Satisfied();
        };

        It should_return_false = () => { result.ShouldBeFalse(); };
    }
}
