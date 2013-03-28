using Machine.Specifications;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using MFlow.Core.Validation.Builder;
using MFlow.Core.Validation.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation.given
{
    [Subject("for Fluent Validation")]
    public class a_fluent_user_validator_with_satisfied_dependency
    {
        protected static IFluentValidationBuilder<User> validator;
        protected static IFluentValidationBuilder<User> satisfied_dependency;
        protected static User user; 

        Establish context = () =>
        {
            user = new User();
            validator = new FluentValidationFactory().CreateFor<User>(user);
            satisfied_dependency = new FluentValidationFactory().CreateFor<User>(user);
        };
    }
}
