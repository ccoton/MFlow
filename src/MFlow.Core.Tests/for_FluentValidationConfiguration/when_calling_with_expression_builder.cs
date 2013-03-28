using Machine.Specifications;
using MFlow.Core.ExpressionBuilder;

namespace MFlow.Core.Tests.for_FluentValidationConfiguration
{
    [Subject("for_FluentValidationConfiguration")]
    class when_calling_with_expression_builder
    {
        Because of = () =>
        {
            MFlowConfiguration.Current.WithExpressionBuilder(
                new ExpressionBuilderConfiguration(new CustomExpressionBuilder()));
        };

        It should_set_the_message_resolver = () =>
        {
            MFlowConfiguration.Current.ExpressionBuilder.Builder.ShouldBeOfType<CustomExpressionBuilder>();
        };

    }
}
