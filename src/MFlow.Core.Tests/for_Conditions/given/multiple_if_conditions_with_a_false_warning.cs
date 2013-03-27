﻿using Machine.Specifications;
using MFlow.Core.Conditions;
using MFlow.Core.Conditions.Enums;
using MFlow.Core.Validation.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_Conditions.given
{
    [Subject("for Conditions")]
    public class multiple_if_conditions_with_a_false_warning
    {
        protected static IFluentConditions<object> fluent_conditions;

        Establish context = () =>
        {
            var expressionBuilder = Configuration.Current.ExpressionBuilderConfiguration.Builder;
            fluent_conditions = new FluentConditions<object>(new object(), expressionBuilder);
            fluent_conditions.If(1 == 1).And(1 == 1).And(false, output: ConditionOutput.Warning);
        };
    }
}
