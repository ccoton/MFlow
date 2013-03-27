using Machine.Specifications;
using MFlow.Core.ExpressionBuilder;
using MFlow.Core.MessageResolver;
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

        It should_have_a_message_resolver = () =>
        {
            Configuration.Current.MessageResolverConfiguration.Resolver.ShouldBeOfType<IResolveValidationMessages>();
        };

        It should_have_an_message_resolver = () =>
        {
            Configuration.Current.ExpressionBuilderConfiguration.Builder.ShouldBeOfType<IBuildExpressions>();
        };
    }
}
