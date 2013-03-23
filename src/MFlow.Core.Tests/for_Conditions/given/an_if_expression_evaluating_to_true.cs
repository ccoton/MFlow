using Machine.Specifications;
using MFlow.Core.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_Conditions.given
{
    [Subject("for Conditions")]
    public class an_if_expression_evaluating_to_true
    {
        protected static IFluentConditions<object> fluent_conditions;

        Establish context = () =>
        {
            var test_object = new object();
            fluent_conditions = new FluentConditions<object>(test_object);
            fluent_conditions.If(o => o.Equals(test_object));
        };
    }
}
