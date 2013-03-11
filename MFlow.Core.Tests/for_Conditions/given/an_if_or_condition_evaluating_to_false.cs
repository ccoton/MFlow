﻿using Machine.Specifications;
using MFlow.Core.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_Conditions.given
{
    public class an_if_or_condition_evaluating_to_false
    {
        protected static IFluentConditions<object> fluent_conditions;

        Establish context = () =>
        {
            fluent_conditions = new FluentConditions<object>(new object());
            fluent_conditions.If(1 == 2).Or(1 == 2);
        };
    }
}
