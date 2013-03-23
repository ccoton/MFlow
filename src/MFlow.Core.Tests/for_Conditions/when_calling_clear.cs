using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_Conditions
{
    public class when_calling_clear : given.an_if_or_condition_evaluating_to_true
    {
        Because of = () =>
        {
            fluent_conditions.Clear();
        };

        It should_remove_all_conditions = () => { fluent_conditions.Conditions.Count.ShouldEqual(0); };
    }
}
