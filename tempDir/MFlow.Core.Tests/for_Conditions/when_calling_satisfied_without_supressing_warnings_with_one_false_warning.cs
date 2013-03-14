using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_Conditions
{
    public class when_calling_satisfied_without_suppressing_warnings_with_one_false_warning : given.multiple_if_conditions_with_a_false_warning
    {
        static bool result;

        Because of = () =>
        {
            result = fluent_conditions.Satisfied(suppressWarnings: false);
        };

        It should_not_suppress_warnings = () => { result.ShouldBeFalse(); };
    }
}
