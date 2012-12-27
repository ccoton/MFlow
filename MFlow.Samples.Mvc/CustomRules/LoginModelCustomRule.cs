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
        private readonly IFluentValidationFactory _factory;

        public LoginModelCustomRule()
        {
            _factory = new FluentValidationFactory();
        }

        public bool Execute(LoginModel target)
        {
            return _factory.GetFluentValidation<LoginModel>(target)
                .If(UsernameService.UsernameAvailable(target.UserName))
                .Satisfied();
        }
    }


    /// <summary>
    ///     Just a dummy class as an example
    /// </summary>
    class UsernameService
    {
        public static bool UsernameAvailable(string username)
        {
            return false;
        }
    }
}