using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_group_with_no_checks : given.a_fluent_user_validator
    {

        static Exception exception;

        Because of = () => { exception = Catch.Exception(() => { validator.Group("Test"); }); };

        It should_throw_an_application_exception = () => { exception.ShouldBeOfType<ApplicationException>(); };
        It should_give_a_description_error_message = () => { exception.Message.ShouldEqual("Calling Group on a validator is only valid once the validator has at least one context, i.e. Check has been called"); };
    }
}
