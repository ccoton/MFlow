using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_hint_with_a_custom_hint : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            validator.Check(u => u.Username).IsNotEmpty().Hint("A custom hint");
        };

        It should_resolve_the_custom_hint = () => { validator.Validate().First().Condition.Hint.ShouldEqual("A custom hint"); };

    }
}
