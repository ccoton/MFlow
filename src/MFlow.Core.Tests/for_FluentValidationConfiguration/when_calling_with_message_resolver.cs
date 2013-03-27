using Machine.Specifications;
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
    class when_calling_with_message_resolver
    {
        Because of = () => { Configuration.Current.WithMessageResolver(
            new MessageResolverConfiguration(new CustomMessageResolver())); };

        It should_set_the_message_resolver = () =>
        {
            Configuration.Current.MessageResolverConfiguration.Resolver.ShouldBeOfType<CustomMessageResolver>();
        };

    }
}
