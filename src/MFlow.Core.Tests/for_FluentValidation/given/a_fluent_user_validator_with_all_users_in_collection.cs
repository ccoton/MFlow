using Machine.Specifications;
using MFlow.Core.Internal.Validators.Strings;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using MFlow.Core.Validation.Builder;
using MFlow.Core.Validation.Configuration;
using MFlow.Core.Validation.Configuration.Enums;
using MFlow.Core.Validation.Factories;

namespace MFlow.Core.Tests.for_FluentValidation.given
{
    [Subject("for Fluent Validation")]
    public class a_fluent_user_validator_with_all_users_in_collection
    {
        protected static IFluentValidationBuilder<User> validator;
        
        protected static User user1;
        protected static User user2;

        Establish context = () =>
            {
                Configuration.Current.WithCustomImplementationMode(CustomImplementationMode.Ignore);
                user1 = new User
                    {
                        Username = "testing1"
                    };

                user2 = new User
                    {
                        Username = "testing2"
                    };

                user1.Users.Add(user1);
                user1.Users.Add(user2);
                validator = new FluentValidationFactory().GetFluentValidation<User>(user1);
            };
    }
}