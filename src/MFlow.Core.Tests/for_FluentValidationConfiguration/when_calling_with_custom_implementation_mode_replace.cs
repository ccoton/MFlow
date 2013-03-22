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
    class when_callin_with_custom_implementation_mode_replace
    {
        Because of = () => { Configuration.Current.WithCustomImplementationMode(CustomImplementationMode.Replace); };

        It should_have_a_custom_implemention_mode_of_replace = () =>
        {
            Configuration.Current.CustomImplementationMode.ShouldEqual(CustomImplementationMode.Replace);
        };

    }
}
