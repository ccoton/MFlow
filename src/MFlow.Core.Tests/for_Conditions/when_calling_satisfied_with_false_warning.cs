using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_Conditions
{
    public class when_calling_satisfied_with_false_warning : given.multiple_if_conditions_with_a_false_warning
    {
        static bool result;

        Because of = () =>
        {
            result = fluent_conditions.Satisfied();
        };

        It should_suppress_warnings = () => { result.ShouldBeTrue(); };
    }
}
