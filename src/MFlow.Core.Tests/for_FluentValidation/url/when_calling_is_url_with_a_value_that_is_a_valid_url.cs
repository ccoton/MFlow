using Machine.Specifications;
using MFlow.Core.Configuration.Enums;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_url_with_a_value_that_is_a_valid_url : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            MFlowConfiguration.Current.WithCustomImplementationMode(CustomImplementationMode.Ignore);
            user.Username = "http://www.google.com";
            validator.Check(u => u.Username).IsUrl();
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };

    }
}
