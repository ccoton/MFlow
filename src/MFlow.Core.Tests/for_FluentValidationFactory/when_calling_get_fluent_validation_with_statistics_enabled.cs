using Machine.Specifications;
using MFlow.Core.Statistics;
using MFlow.Core.Validation;
using MFlow.Core.Validation.Builder;

namespace MFlow.Core.Tests.for_FluentValidationFactory
{
    public class when_calling_get_fluent_validation_with_statistics_enabled : given.a_fluent_validation_factory
    {
        static IFluentValidationBuilder<object> validation;
        Because of = () => {
            MFlowConfiguration.Current.WithStatistics(new StatisticsConfiguration(new NullValidationStatisticsRecorder()));
            validation = validation_factory.CreateFor<object>(new object());
            MFlowConfiguration.Current.WithDefaults();
        };
        
        It should_return_fluent_validation_statistics_implementation = () => { validation.ShouldBeOfType<FluentValidationWithStatistics<object>>(); };
    }
}
