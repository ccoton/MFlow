using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Mvc.Tests.Supporting
{
    public class LoginViewModel : ValidatedModel<LoginViewModel>
    {
        public LoginViewModel()
        {
            SetTarget(this);

            Validator
                .Check(u => u.Username).IsNotEmpty().Message("Username should not be empty, please enter a username")
                .Check(u => u.Password).IsNotEmpty().Message("Password should not be empty, please enter a password");
            
        }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
