﻿using Machine.Specifications;
using MFlow.Core.Configuration.Enums;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation.Builder;
using MFlow.Core.Validation.Factories;

namespace MFlow.Core.Tests.for_FluentValidation.given
{
    [Subject("for Fluent Validation")]
    public class a_fluent_user_validator_with_custom_implementation_set_to_replace
    {
        protected static IFluentValidationBuilder<User> validator;
        protected static User user; 

        Establish context = () =>
        {
            MFlowConfiguration.Current.WithCustomImplementationMode(CustomImplementationMode.Replace);
            user = new User();
            validator = new FluentValidationFactory().CreateFor<User>(user);
        };
    }
}
