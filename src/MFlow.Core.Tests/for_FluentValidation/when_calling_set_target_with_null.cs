using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_set_target_with_null : given.a_fluent_user_validator
    {
        static Exception exception; 

        Because of = () =>
        {
            exception = Catch.Exception(() => { validator.SetTarget(null); });
        };

        It should_throw_an_argument_null_exception = () => { exception.ShouldBeOfType<ArgumentNullException>(); };
    }
}
