using System;
using MFlow.Core.Validation.Factories;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;

namespace MFlow.Core.Tests.CustomRules
{
    public class CheckCustomCondition : IFluentValidationCustomRule<User>
    {
        readonly IFluentValidationFactory _factory;
        public CheckCustomCondition()
        {
            _factory = new FluentValidationFactory();
        }

        public IFluentValidation<User> Execute(Func<User> targetFunc)
        {
            var target = targetFunc();
            var someCrazyCustomConditional = target.LoginCount == 999;
            return _factory.CreateFor<User>(target)
                .If(someCrazyCustomConditional).Key("UserName").Message("The crazy conditional");
        }
    }
}
