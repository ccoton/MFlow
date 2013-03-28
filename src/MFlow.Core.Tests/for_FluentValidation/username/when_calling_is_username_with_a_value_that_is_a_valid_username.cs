using Machine.Specifications;
using MFlow.Core.Configuration.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_username_with_a_value_that_is_a_valid_username : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            MFlowConfiguration.Current.WithCustomImplementationMode(CustomImplementationMode.Ignore);
            user.Username = "mark-woodhall";
            validator.Check(u => u.Username).IsUsername();
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };

    }
}
