using System;
using MFlow.Core.Validation;
using MFlow.Samples.Mvc.Models;
using MFlow.Core.Validation.Factories;

namespace MFlow.Samples.Mvc.CustomRules
{
    public class CheckUsernameAvailability :  IFluentValidationCustomRule<RegisterModel>
    {
        readonly IFluentValidationFactory _factory;

        public CheckUsernameAvailability()
        {
            _factory = new FluentValidationFactory();
        }

        public IFluentValidation<RegisterModel> Execute(Func<RegisterModel> targetFunc)
        {
            var target = targetFunc();
            return _factory.CreateFor(target)
                .If(UsernameService.UsernameAvailable(target.UserName)) 
                    .Key("UserName")
                    .Message(string.Format("Try {0}", UsernameService.SuggestedUsername(target.UserName)));
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

        public static string SuggestedUsername(string username)
        {
            return string.Format("{0}.foo", username);
        }
    }
}