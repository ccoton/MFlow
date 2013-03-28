using MFlow.Mvc;

namespace MFlow.WebApi.Tests
{
    public class User : ValidatedModel<User>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public User()
        {
            GetValidator(this)
                .Check(m => m.Username).IsNotEmpty().Hint("Enter a username")
                .Check(m => m.Password).IsNotEmpty().Hint("Enter a password");
        }
    }
}

