using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_satisfied_with_multiple_true_conditions : given.a_fluent_user_validator_with_multiple_true_conditions
    {

        static bool satisfied;

        Because of = () =>
        {
            satisfied = validator.Satisfied();
        };

        It should_be_satisfied = () => { satisfied.ShouldBeTrue(); };

    }
}
