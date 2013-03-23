using Machine.Specifications;
using MFlow.Core.Validation.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidationFactory.given
{
    [Subject("for Fluent Validation Factory")]
    public class a_fluent_validation_factory
    {
        protected static IFluentValidationFactory validation_factory;

        Establish context = () =>
        {
            validation_factory = new FluentValidationFactory();
        };
    }
}
