using Machine.Specifications;
using MFlow.Core.Tests.Supporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_ValidationResult
{
    public class when_creating_with_null_argument
    {
        static Exception exception = null;

        Because of = () =>
        {
            exception = Catch.Exception(() => { new MFlow.Core.Validation.ValidationResult<User>(null); });
        };

        It should_throw_an_argument_null_exception = () => { exception.ShouldBeOfType<ArgumentNullException>(); };
    }
}
