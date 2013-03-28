using Machine.Specifications;
using MFlow.Core.Validation;
using MFlow.Core.Validation.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidationFactory
{
    public class when_calling_get_fluent_validation : given.a_fluent_validation_factory
    {
        static IFluentValidationBuilder<object> validation;
        Because of = () => { validation = validation_factory.CreateFor<object>(new object()); };
        It should_return_fluent_validation_implementation = () => { validation.ShouldBeOfType<IFluentValidationBuilder<object>>(); };
    }
}
