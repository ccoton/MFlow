using Machine.Specifications;
using MFlow.Core.MessageResolver;
using MFlow.Core.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidationConfiguration
{
    [Subject("for_FluentValidationConfiguration")]
    class when_calling_with_message_resolver
    {
        Because of = () =>
        {
            MFlowConfiguration.Current.WithMessageResolver(
            new MessageResolverConfiguration(new CustomMessageResolver())); };

        It should_set_the_message_resolver = () =>
        {
            MFlowConfiguration.Current.MessageResolver.Resolver.ShouldBeOfType<CustomMessageResolver>();
        };

    }
}
