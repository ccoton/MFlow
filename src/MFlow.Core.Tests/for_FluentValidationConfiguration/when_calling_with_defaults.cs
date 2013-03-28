using Machine.Specifications;
using MFlow.Core.Configuration.Enums;
using MFlow.Core.ExpressionBuilder;
using MFlow.Core.MessageResolver;

namespace MFlow.Core.Tests.for_FluentValidationConfiguration
{
    [Subject("for_FluentValidationConfiguration")]
    class when_callin_with_with_defaults
    {
        Because of = () => { MFlowConfiguration.Current.WithDefaults(); };

        It should_have_a_custom_implemention_mode_of_ignore = () =>
        {
            MFlowConfiguration.Current.CustomImplementationMode.ShouldEqual(CustomImplementationMode.Ignore);
        };

        It should_have_statistics_enabled = () =>
        {
            MFlowConfiguration.Current.Statistics.Enabled.ShouldEqual(true);
        };

        It should_have_a_message_resolver = () =>
        {
            MFlowConfiguration.Current.MessageResolver.Resolver.ShouldBeOfType<IResolveValidationMessages>();
        };

        It should_have_an_message_resolver = () =>
        {
            MFlowConfiguration.Current.ExpressionBuilder.Builder.ShouldBeOfType<IBuildExpressions>();
        };
    }
}
