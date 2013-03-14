using Machine.Specifications;
using MFlow.Core.Conditions;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using MFlow.Core.Validation.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_clear : given.a_fluent_user_validator
    {
        Because of = () =>
        {
            user = new User();
            ((IFluentValidation<User>)validator).Clear();
        };

        It should_remove_all_the_conditions = () => { ((IFluentConditions<User>)validator).Conditions.Count.ShouldEqual(0); };
    }
}
