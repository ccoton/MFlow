using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    class when_calling_check_with_nullables : given.a_fluent_user_validator
    {
        static Exception exception;

        Because of = () =>
        {
            exception = Catch.Exception(() =>
            {
                validator
                    .Check(u => u.LockedOutCount).IsGreaterThan(0)
                    .Check(u => u.LastLogin).IsToday();
            });
        };

        It should_not_throw_an_exception = () => { exception.ShouldBeNull(); };
    }
}
