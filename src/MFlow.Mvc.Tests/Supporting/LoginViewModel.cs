﻿namespace MFlow.Mvc.Tests.Supporting
{
    public class LoginViewModel : ValidatedModel<LoginViewModel>
    {
        public LoginViewModel()
        {
            GetValidator(this)
                .Check(u => u.Username).IsNotEmpty().Message("Username should not be empty, please enter a username").Hint("Please enter a username")
                .Check(u => u.Password).IsNotEmpty().Message("Password should not be empty, please enter a password").Hint("Please enter a password");            
        }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
