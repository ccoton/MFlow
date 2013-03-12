using Machine.Specifications;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_depends_on_with_an_unsatisfied_dependency : given.a_fluent_user_validator_with_unsatisfied_dependency
    {

        Because of = () =>
        {
            user.Password = "password";
            validator.Check(u => u.Password).IsRequired<string>().DependsOn((IFluentValidation<User>)unsatisfied_dependency);
        };

        It should_not_be_satisfied = () => { validator.Satisfied().ShouldBeFalse(); };

    }
}
