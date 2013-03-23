using Machine.Specifications;
using MFlow.Core.Validation.Configuration;
using MFlow.Core.Validation.Configuration.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidationConfiguration
{
    [Subject("for_FluentValidationConfiguration")]
    class when_calling_without_statistics
    {
        Because of = () => { Configuration.Current.WithoutStatistics(); };

        It should_have_statistics_disabled = () =>
        {
            Configuration.Current.StatisticsEnabled.ShouldEqual(false);
        };

    }
}
