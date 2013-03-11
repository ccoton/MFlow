using Machine.Specifications;
using MFlow.Core.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_Conditions.given
{
    public class multiple_if_conditions_evaluating_to_true
    {
        protected static IFluentConditions<object> fluent_conditions;

        Establish context = () =>
        {
            fluent_conditions = new FluentConditions<object>(new object());
            fluent_conditions.If(1 == 1).And(1 == 1).And(true);
        };
    }
}
