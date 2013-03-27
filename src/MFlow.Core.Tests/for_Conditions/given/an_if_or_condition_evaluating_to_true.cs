using Machine.Specifications;
using MFlow.Core.Conditions;
using MFlow.Core.Validation.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_Conditions.given
{
    [Subject("for Conditions")]
    public class an_if_or_condition_evaluating_to_true
    {
        protected static IFluentConditions<object> fluent_conditions;

        Establish context = () =>
        {
            var expressionBuilder = Configuration.Current.ExpressionBuilderConfiguration.Builder;
            fluent_conditions = new FluentConditions<object>(new object(), expressionBuilder);
            fluent_conditions.If(1 == 2).Or(1 ==1);
        };
    }
}
