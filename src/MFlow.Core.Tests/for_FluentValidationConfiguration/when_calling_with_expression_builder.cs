using Machine.Specifications;
using MFlow.Core.ExpressionBuilder;
using MFlow.Core.MessageResolver;
using MFlow.Core.Statistics;
using MFlow.Core.Validation.Configuration;
using MFlow.Core.Validation.Configuration.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidationConfiguration
{
    [Subject("for_FluentValidationConfiguration")]
    class when_calling_with_expression_builder
    {
        Because of = () =>
        {
            Configuration.Current.WithExpressionBuilder(
                new ExpressionBuilderConfiguration(new CustomExpressionBuilder()));
        };

        It should_set_the_message_resolver = () =>
        {
            Configuration.Current.ExpressionBuilderConfiguration.Builder.ShouldBeOfType<CustomExpressionBuilder>();
        };

    }
}
