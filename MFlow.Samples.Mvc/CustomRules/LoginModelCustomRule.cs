using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MFlow.Core.Validation;
using MFlow.Samples.Mvc.Models;

namespace MFlow.Samples.Mvc.CustomRules
{
    public class LoginModelCustomRule :  IFluentValidationCustomRule<LoginModel>
    {
        public LoginModelCustomRule()
        {
        }

        public IFluentValidation<LoginModel> Execute(IFluentValidation<LoginModel> validator)
        {
            var target = validator.GetTarget();
            validator
                .If(UsernameService.CheckUsernameExists(target.UserName), "UserName", "The username is already in use")
                .If(UsernameService.SuggestUsernames(), "UserName", string.Format("Try {0}", UsernameService.SuggestUsername(target.UserName)));

            return validator;
        }
    }


    /// <summary>
    ///     Just a dummy class as an example
    /// </summary>
    class UsernameService
    {
        public static bool CheckUsernameExists(string username)
        {
            return true;
        }

        public static bool SuggestUsernames()
        {
            return true;
        }

        public static string SuggestUsername(string username)
        {
            return string.Format("{0}.blah", username);
        }
    }
}