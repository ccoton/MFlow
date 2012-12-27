using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;

namespace MFlow.Core.Tests.CustomRules
{
    public class UserCustomRule : IFluentValidationCustomRule<User>
    {
        public UserCustomRule()
        {
        }

        public IFluentValidation<User> Execute(IFluentValidation<User> validator)
        {
            var target = validator.GetTarget();
            var someCrazyCustomConditional = validator.GetTarget().LoginCount == 999;
            return validator
                .If(someCrazyCustomConditional, "UserName", "The crazy conditional");
        }
    }
}
