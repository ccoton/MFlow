using Machine.Specifications;
using MFlow.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.WebApi.Tests.for_ValidationController
{
    public class when_posting_with_invalid_type : given.a_validation_controller
    {
        static Exception exception = null;

        Because of = () => { exception = Catch.Exception(() => { validation_controller.Post(new ModelToValidate() { Validate = new object(), Type = "" }); }); };

        It should_throw_an_argument_exception = () => { exception.ShouldBeOfType<ArgumentException>(); };
    }
}
