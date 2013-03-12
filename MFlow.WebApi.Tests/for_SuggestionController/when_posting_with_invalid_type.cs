using Machine.Specifications;
using MFlow.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.WebApi.Tests.for_SuggestionController
{
    public class when_posting_with_invalid_type : given.a_suggestion_controller
    {
        static Exception exception = null;

        Because of = () => { exception = Catch.Exception(() => { suggestion_controller.Post(new ModelToValidate() { Validate = new object(), Type = "" }); }); };

        It should_throw_an_argument_exception = () => { exception.ShouldBeOfType<ArgumentException>(); };
    }
}
