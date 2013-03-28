using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation.Builder;
using MFlow.Core.Validation.Factories;
using Machine.Specifications;

namespace MFlow.Core.Tests.for_FluentValidation.given
{
    [Subject("for Fluent Validation")]
    public class a_fluent_user_validator_with_all_users_not_in_collection
    {
        protected static IFluentValidationBuilder<User> validator;
        protected static User user1;
        protected static User user2;

        Establish context = () =>
            {
                user1 = new User
                    {
                        Username = "testing1"
                    };

                user2 = new User
                    {
                        Username = "testing2"
                    };

                user1.Users.Add(user1);
                validator = new FluentValidationFactory().CreateFor<User>(user1);
            };
    }
}