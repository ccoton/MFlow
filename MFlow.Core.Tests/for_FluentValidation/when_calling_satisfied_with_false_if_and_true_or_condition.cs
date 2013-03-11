using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_satisfied_with_false_if_and_true_or_condition : given.a_fluent_user_validator_with_false_if_and_true_or_conditions
    {

        static bool satisfied;

        Because of = () =>
        {
            satisfied = validator.Satisfied();
        };

        It should_be_satisfied = () => { satisfied.ShouldBeFalse(); };


    }
}
