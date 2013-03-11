using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_this_year_with_a_date_that_is_this_year : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.LastLogin = DateTime.Now;
            validator.Check(u => u.LastLogin).IsThisYear();
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };
    }
}
