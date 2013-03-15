using Machine.Specifications;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_satisfied_without_specifying_group_with_one_true_and_one_false_group : given.a_fluent_user_validator_with_one_true_and_one_false_group
    {

        static bool satisfied;

        Because of = () => {
            satisfied = validator.Satisfied();
        };

        It should_not_be_satisfied = () => { satisfied.ShouldBeFalse(); };
    }
}
