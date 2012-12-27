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
        private readonly IFluentValidationFactory _factory;

        public UserCustomRule()
        {
            _factory = new FluentValidationFactory();
        }

        public bool Execute(User target)
        {
            var someCrazyCustomConditional = target.LoginCount == 999;
            return _factory.GetFluentValidation<User>(target)
                .If(someCrazyCustomConditional, "UserName", "The crazy conditional")
                .Satisfied();
        }
    }
}
