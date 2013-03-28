using Machine.Specifications;
using MFlow.Core.Conditions;
using MFlow.Core.Tests.Supporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_Conditions.given
{
    [Subject("for Conditions")]
    public class an_if_expression_evaluating_to_false
    {
        protected static IFluentConditions<User> fluent_conditions;

        Establish context = () =>
        {
            var expressionBuilder = MFlowConfiguration.Current.ExpressionBuilder.Builder;
            var test_object = new User() { IsActive = false };
            fluent_conditions = new FluentConditions<User>(test_object, expressionBuilder);
            fluent_conditions.If(r => r.IsActive);
        };
    }
}
