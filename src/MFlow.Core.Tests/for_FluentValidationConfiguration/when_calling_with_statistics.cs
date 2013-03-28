using Machine.Specifications;
using MFlow.Core.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidationConfiguration
{
    [Subject("for_FluentValidationConfiguration")]
    class when_calling_with_statistics
    {
        Because of = () => { MFlowConfiguration.Current.WithStatistics(new StatisticsConfiguration(new NullValidationStatisticsRecorder())); };

        It should_have_statistics_enabled = () =>
        {
            MFlowConfiguration.Current.Statistics.Enabled.ShouldEqual(true);
        };

    }
}
