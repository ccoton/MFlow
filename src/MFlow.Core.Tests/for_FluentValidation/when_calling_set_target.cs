using Machine.Specifications;
using MFlow.Core.Tests.Supporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_set_target : given.a_fluent_user_validator
    {
        Because of = () =>
        {
            user = new User();
            validator.SetTarget(user);
        };

        It should_set_the_target = () => { validator.GetTarget().ShouldBeTheSameAs(user); };
    }
}
