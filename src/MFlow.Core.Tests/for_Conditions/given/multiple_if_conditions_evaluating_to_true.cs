using Machine.Specifications;
using MFlow.Core.Conditions;

namespace MFlow.Core.Tests.for_Conditions.given
{
    [Subject("for Conditions")]
    public class multiple_if_conditions_evaluating_to_true
    {
        protected static IFluentConditions<object> fluent_conditions;

        Establish context = () =>
        {
            var expressionBuilder = MFlowConfiguration.Current.ExpressionBuilder.Builder;
            fluent_conditions = new FluentConditions<object>(new object(), expressionBuilder);
            fluent_conditions.If(1 == 1).And(1 == 1).And(true);
        };
    }
}
