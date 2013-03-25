﻿using Machine.Specifications;
using MFlow.Core.Validation.Configuration;
using MFlow.Core.Validation.Configuration.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidationConfiguration
{
    [Subject("for_FluentValidationConfiguration")]
    class when_callin_with_with_defaults
    {
        Because of = () => { Configuration.Current.WithDefaults(); };

        It should_have_a_custom_implemention_mode_of_ignore = () =>
        {
            Configuration.Current.CustomImplementationMode.ShouldEqual(CustomImplementationMode.Ignore);
        };

        It should_have_statistics_enabled = () =>
        {
            Configuration.Current.StatisticsEnabled.ShouldEqual(true);
        };

    }
}