using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_is_credit_card_with_a_value_that_is_a_credit_card_number : given.a_fluent_user_validator
    {

        Because of = () =>
        {
            user.Username = "5105 1051 0510 5100";
            validator.Check(u => u.Username).IsCreditCard();
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };

    }
}
