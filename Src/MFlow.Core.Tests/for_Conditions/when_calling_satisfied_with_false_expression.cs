using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_Conditions
{
    public class when_calling_satisfied_with_false_expression : given.an_if_expression_evaluating_to_false
    {
        static bool result;

        Because of = () =>
        {
            result = fluent_conditions.Satisfied();
        };

        It should_return_false = () => { result.ShouldBeFalse(); };
    }
}
