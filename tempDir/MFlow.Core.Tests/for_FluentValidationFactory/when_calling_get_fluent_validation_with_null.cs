using Machine.Specifications;
using MFlow.Core.Validation;
using MFlow.Core.Validation.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidationFactory
{
    public class when_calling_get_fluent_validation_with_null : given.a_fluent_validation_factory
    {
        static Exception exception = null;
        Because of = () => { exception = Catch.Exception(() => { validation_factory.GetFluentValidation<object>(null); }); };
        It should_return_throw_an_argument_null_exception = () => { exception.ShouldBeOfType<ArgumentNullException>(); };
    }
}
