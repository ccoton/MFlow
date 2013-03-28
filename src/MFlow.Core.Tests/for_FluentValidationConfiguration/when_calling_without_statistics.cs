using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidationConfiguration
{
    [Subject("for_FluentValidationConfiguration")]
    class when_calling_without_statistics
    {
        Because of = () => { MFlowConfiguration.Current.WithoutStatistics(); };

        It should_have_statistics_disabled = () =>
        {
            MFlowConfiguration.Current.Statistics.Enabled.ShouldEqual(false);
        };

    }
}
