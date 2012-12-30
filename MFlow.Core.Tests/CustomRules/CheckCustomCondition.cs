using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;

namespace MFlow.Core.Tests.CustomRules
{
    public class CheckCustomCondition : IFluentValidationCustomRule<User>
    {
        private readonly IFluentValidationFactory _factory;
        public CheckCustomCondition()
        {
            _factory = new FluentValidationFactory();
        }

        public IFluentValidation<User> Execute(Func<User> targetFunc)
        {
            var target = targetFunc();
            var someCrazyCustomConditional = target.LoginCount == 999;
            return _factory.GetFluentValidation<User>(target)
                .Check(someCrazyCustomConditional, "UserName", "The crazy conditional");
               // .Satisfied();
        }
    }
}
